using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.ViewModels
{
    public class TestSessionSummaryDTO
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public int QuestionsCount { get; set; }
        public bool IsActive { get; set; }
    }
}
