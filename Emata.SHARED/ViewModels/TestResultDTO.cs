using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.ViewModels
{
    public class TestResultDTO
    {
        public Guid SessionId { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double Score { get { return (double)CorrectAnswers / TotalQuestions * 100; } }
        public TimeSpan TimeTaken { get; set; }
    }
}
