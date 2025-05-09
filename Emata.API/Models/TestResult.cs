using Emata.API.Models.Shared;

namespace Emata.API.Models
{
    public class TestResult : Entity
    {
        public Guid SessionId { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double Score { get { return (double)CorrectAnswers / TotalQuestions * 100; } }
        public TimeSpan TimeTaken { get; set; }

        // Navigation property
        public virtual TestSession? Session { get; set; }
    }
}
