using Emata.API.Models;
using Emata.Shared.ViewModels;

namespace Emata.API.Extensions
{
    internal static class SessionMappingExtension
    {
        public static TestSessionDTO ToTestSessionDTO(this TestSession session)
            => new TestSessionDTO
            {
                Id = session.Id,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Questions = session.SessionQuestions.OrderBy(sq => sq.OrderIndex)
                .Select(sq => sq.Question).ToList().Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    Options = q.Options,
                    SelectedOptionIndex = session.UserAnswers.TryGetValue(q.Id, out var answer) ? answer : null,
                }).ToList(),
                UserAnswers = session.UserAnswers
            };

        public static TestResultDTO ToTestResultDTO(this TestResult result)
            => new TestResultDTO
            {
                SessionId = result.SessionId,
                CompletedAt = result.CompletedAt,
                TotalQuestions = result.TotalQuestions,
                CorrectAnswers = result.CorrectAnswers,
                TimeTaken = result.TimeTaken
            };
    }
}
