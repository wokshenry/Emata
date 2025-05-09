using Emata.API.Configuration.EndpointConfiguration;
using Emata.API.Extensions;
using Emata.API.Models;
using Emata.API.Services;
using Emata.Shared.ViewModels;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Emata.API.Endpoints
{
    internal class SessionEndPoint : IEndpoints
    {
        public const string Tag = "Session";
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            //get start-test-session
            app.MapPost($"/session/start-test-session", async Task<Results<Ok<TestSessionDTO>, EmptyHttpResult, ProblemHttpResult>> (
                [FromServices] ITestSessionService _testSessionService,
            [FromServices] ILogger<SessionEndPoint> _logger) =>
            {
                var session = await _testSessionService.CreateNewSessionAsync();

                _logger.LogInformation($"Starting new test session: {session.Id}");

                return TypedResults.Ok(session.ToTestSessionDTO());

            })
                //.RequireAuthorization() //todo, consider 
            .WithTags(Tag);

            //get get-session
            app.MapGet($"/session/get-session/{{sessionId}}", async Task<Results<Ok<TestSessionDTO>, EmptyHttpResult, ProblemHttpResult>> (
                Guid sessionId,
                [FromServices] ITestSessionService _testSessionService,
            [FromServices] ILogger<SessionEndPoint> _logger) =>
            {
                var session = await _testSessionService.GetSessionAsync(sessionId);
                if (session == null)
                {
                    _logger.LogWarning($"Session not found or expired: {sessionId}");

                    return TypedResults.Problem("Session not found or expired", statusCode: StatusCodes.Status404NotFound);
                }

                return TypedResults.Ok(session.ToTestSessionDTO());

            })
            //.RequireAuthorization() //todo, consider 
            .WithTags(Tag);

            //submit-answer
            app.MapPost($"/session/submit-answer", async Task<Results<Ok, EmptyHttpResult, ProblemHttpResult>> (
                [FromBody] AnswerSubmitDTO model,
                [FromServices] ITestSessionService _testSessionService,
            [FromServices] ILogger < SessionEndPoint > _logger) =>
            {

                _logger.LogInformation($"Submitting answer for session {model.SessionId}, question {model.QuestionId}: {model.SelectedOptionIndex}");

                var success = await _testSessionService.SubmitAnswerAsync(
                    model.SessionId, model.QuestionId, model.SelectedOptionIndex);

                if (!success)
                {
                    _logger.LogWarning($"Failed to submit answer: session {model.SessionId} expired or not found");
                    return TypedResults.Problem("Session not found or expired", statusCode: StatusCodes.Status400BadRequest);
                }

                return TypedResults.Ok();

            })
                //.RequireAuthorization()
            .WithTags(Tag);

            //finish-test-session
            app.MapPost($"/session/finish-test-session/{{sessionId}}", async Task<Results<Ok<TestResultDTO>, EmptyHttpResult, ProblemHttpResult>> (
                Guid sessionId,
                [FromServices] ITestSessionService _testSessionService,
            [FromServices] ILogger<SessionEndPoint> _logger) =>
            {

                _logger.LogInformation($"Finishing test session: {sessionId}");

                try
                {
                    var result = await _testSessionService.CalculateResultsAsync(sessionId);
                    return TypedResults.Ok(result.ToTestResultDTO());
                }
                catch (ArgumentException ex)
                {
                    _logger.LogWarning($"Failed to finish test: {ex.Message}");
                    
                    return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status400BadRequest);
                }
            })
            //.RequireAuthorization()
            .WithTags(Tag);

            //get get-active-session
            app.MapGet($"/session/get-active-session", async Task<Results<Ok<List<TestSessionSummaryDTO>>, EmptyHttpResult, ProblemHttpResult>> (
                [FromServices] ITestSessionService _testSessionService,
            [FromServices] ILogger<SessionEndPoint> _logger) =>
            {
                var sessions = await _testSessionService.GetActiveSessions();

                var summaries = sessions.Select(s => new TestSessionSummaryDTO
                {
                    Id = s.Id,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    RemainingTime = s.EndTime - DateTime.UtcNow,
                    QuestionsCount = s.SessionQuestions?.Count ?? 0,
                    IsActive = s.IsActive,
                }).ToList();

                return TypedResults.Ok(summaries);
            })
            //.RequireAuthorization() //todo, consider 
            .WithTags(Tag);

            //cleanup-expired
            app.MapPost($"/session/cleanup-expired", async Task<Results<Ok, EmptyHttpResult, ProblemHttpResult>> (
                [FromServices] ITestSessionService _testSessionService,
            [FromServices] ILogger<SessionEndPoint> _logger) =>
            {

                _logger.LogInformation("Running cleanup for expired sessions");

                await _testSessionService.CleanupExpiredSessionsAsync();
                return TypedResults.Ok();
            })
            //.RequireAuthorization()
            .WithTags(Tag);
        }
    }
}
