using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.ViewModels
{
    public class AnswerSubmitDTO
    {
        public Guid SessionId { get; set; }
        public Guid QuestionId { get; set; }
        public int SelectedOptionIndex { get; set; }
    }
}
