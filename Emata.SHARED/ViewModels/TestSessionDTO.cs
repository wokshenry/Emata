using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.ViewModels
{
    public class TestSessionDTO
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<QuestionDTO>? Questions { get; set; }
        public Dictionary<Guid, int?>? UserAnswers { get; set; }
    }
}
