using Emata.Shared.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace Emata.UI.Services
{
    public interface ISessionService
    {
        Task<TestSessionDTO?> StartTestAsync();
        Task<TestSessionDTO?> GetSessionAsync(Guid sessionId);
        Task<bool> SubmitAnswerAsync(Guid sessionId, Guid questionId, int selectedOptionIndex);
        Task<TestResultDTO?> FinishTestAsync(Guid sessionId);
        Task<List<TestSessionSummaryDTO>> GetActiveSessions();
    }

    public class SessionService : ISessionService
    {
        private readonly HttpClient _httpClient;

        public SessionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TestSessionDTO?> StartTestAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("Session/start-test-session", null);

                Console.WriteLine($"Response status: {response.StatusCode}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<TestSessionDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                Console.WriteLine($"Status code: {ex.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting test: {ex.Message}");
                return null;
            }
        }

        public async Task<TestSessionDTO?> GetSessionAsync(Guid sessionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Session/get-session/{sessionId}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<TestSessionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting session: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> SubmitAnswerAsync(Guid sessionId, Guid questionId, int selectedOptionIndex)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(new
                    {
                        SessionId = sessionId,
                        QuestionId = questionId,
                        SelectedOptionIndex = selectedOptionIndex
                    }),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("Session/submit-answer", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting answer: {ex.Message}");
                return false;
            }
        }

        public async Task<TestResultDTO?> FinishTestAsync(Guid sessionId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"Session/finish-test-session/{sessionId}", null);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<TestResultDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finishing test: {ex.Message}");
                return null;
            }
        }

        public async Task<List<TestSessionSummaryDTO>> GetActiveSessions()
        {
            try
            {
                var response = await _httpClient.GetAsync($"Session/get-active-session");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<TestSessionSummaryDTO>>() ?? new List<TestSessionSummaryDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting session: {ex.Message}");
                return new List<TestSessionSummaryDTO>();
            }
        }
    }
}

