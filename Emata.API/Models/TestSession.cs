using Emata.API.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Emata.API.Models
{
    public class TestSession : Entity
    {
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get { return StartTime.AddMinutes(30); } }
        public bool IsActive { get { return DateTime.UtcNow < EndTime; } }
        public bool IsCompleted { get; set; } = false;

        // Navigation property for questions in this session
        public virtual List<TestSessionQuestion>? SessionQuestions { get; set; }

        // JSON serialized dictionary of user answers (QuestionId -> Selected Option Index)
        public string UserAnswersJson { get; set; } = "{}";

        // Helper methods (not stored in DB)
        [NotMapped]
        public Dictionary<Guid, int?> UserAnswers
        {
            get
            {
                if (string.IsNullOrEmpty(UserAnswersJson))
                    return new Dictionary<Guid, int?>();

                return JsonSerializer.Deserialize<Dictionary<Guid, int?>>(UserAnswersJson) ?? new Dictionary<Guid, int?>();
            }
            set
            {
                UserAnswersJson = JsonSerializer.Serialize(value);
            }
        }
    }
}
