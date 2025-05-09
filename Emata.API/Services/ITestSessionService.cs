using Emata.API.Context;
using Emata.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Emata.API.Services
{
    public interface ITestSessionService
    {
        Task<TestSession> CreateNewSessionAsync();
        Task<TestSession?> GetSessionAsync(Guid sessionId);
        Task<bool> SubmitAnswerAsync(Guid sessionId, Guid questionId, int selectedOptionIndex);
        Task<TestResult> CalculateResultsAsync(Guid sessionId);
        Task<List<TestSession>> GetActiveSessions();
        Task CleanupExpiredSessionsAsync();
    }

    public class TestSessionService : ITestSessionService
    {
        private readonly IQuestionService _questionService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TestSessionService> _logger;

        public TestSessionService(
            IQuestionService questionService,
            ApplicationDbContext dbContext,
            ILogger<TestSessionService> logger)
        {
            _questionService = questionService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<TestSession> CreateNewSessionAsync()
        {
            // Create a new test session with 3 random questions
            var questions = await _questionService.GetRandomQuestionsAsync(3);

            var session = new TestSession();
            session.Id = Guid.NewGuid();

            session.SessionQuestions = new List<TestSessionQuestion>();

            // Add questions to the session
            for (int i = 0; i < questions.Count; i++)
            {
                session.SessionQuestions.Add(new TestSessionQuestion
                {
                    Id = Guid.NewGuid(),
                    SessionId = session.Id,
                    QuestionId = questions[i].Id,
                    OrderIndex = i
                });
            }

            // Save to database
            _dbContext.TestSessions.Add(session);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Created new test session: {session.Id}");
            return session;
        }

        public async Task<TestSession?> GetSessionAsync(Guid sessionId)
        {
            // Get session with questions from database
            var session = await _dbContext.TestSessions
                .Include(ts => ts.SessionQuestions)
                .ThenInclude(tsq => tsq.Question)
                .FirstOrDefaultAsync(ts => ts.Id == sessionId);

            if (session == null)
            {
                return null;
            }

            // Check if session is active
            if (!session.IsActive)
            {
                _logger.LogInformation($"Session {sessionId} is expired");
                return null;
            }

            return session;
        }

        public async Task<bool> SubmitAnswerAsync(Guid sessionId, Guid questionId, int selectedOptionIndex)
        {
            var session = await GetSessionAsync(sessionId);
            if (session == null || !session.IsActive)
            {
                return false;
            }

            // Get user answers dictionary
            var userAnswers = session.UserAnswers;

            // Save the answer
            userAnswers[questionId] = selectedOptionIndex;

            // Update the session
            session.UserAnswers = userAnswers;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Answer submitted for session {sessionId}, question {questionId}: {selectedOptionIndex}");

            return true;
        }

        public async Task<TestResult> CalculateResultsAsync(Guid sessionId)
        {
            var sessionResult = await _dbContext.TestResults
                .Include(tr => tr.Session)
                .FirstOrDefaultAsync(tr => tr.SessionId == sessionId);

            if (sessionResult != null)
            {
                _logger.LogInformation($"Session {sessionId} already completed. Score: {sessionResult.Score:F2}%");
                return sessionResult;
            }


                var session = await _dbContext.TestSessions
                .Include(ts => ts.SessionQuestions!)
                .ThenInclude(tsq => tsq.Question)
                .FirstOrDefaultAsync(ts => ts.Id == sessionId);

            if (session == null)
            {
                _logger.LogWarning($"Attempt to calculate results for non-existent session: {sessionId}");
                throw new ArgumentException("Invalid session ID");
            }

            int correctAnswers = 0;
            var userAnswers = session.UserAnswers;

            foreach (var sessionQuestion in session.SessionQuestions!)
            {
                var question = sessionQuestion.Question;
                if (userAnswers.TryGetValue(question!.Id, out var answer) &&
                    answer == question.CorrectOptionIndex)
                {
                    correctAnswers++;
                }
            }

            var timeTaken = DateTime.UtcNow - session.StartTime;

            // Mark session as completed
            session.IsCompleted = true;

            // Create result record
            var result = new TestResult
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                CompletedAt = DateTime.UtcNow,
                TotalQuestions = session.SessionQuestions.Count,
                CorrectAnswers = correctAnswers,
                TimeTaken = timeTaken,
                Session = session
            };

            _dbContext.TestResults.Add(result);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Test completed for session {sessionId}. Score: {result.Score:F2}%, Time: {timeTaken}");

            return result;
        }

        public async Task<List<TestSession>> GetActiveSessions()
        {
            return await _dbContext.TestSessions
                .Where(ts => ts.IsCompleted == false)
                .ToListAsync();
        }

        public async Task CleanupExpiredSessionsAsync()
        {
            var now = DateTime.UtcNow;
            var expiredSessions = await _dbContext.TestSessions
                .Where(ts => !ts.IsCompleted && ts.EndTime < now)
                .ToListAsync();

            foreach (var session in expiredSessions)
            {
                session.IsCompleted = true;
                _logger.LogInformation($"Marking expired session as completed: {session.Id}");
            }

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Cleaned up {expiredSessions.Count} expired sessions");
        }
    }
}
