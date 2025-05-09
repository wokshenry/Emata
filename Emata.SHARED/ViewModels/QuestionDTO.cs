using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.ViewModels
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public List<string>? Options { get; set; }
        public int? SelectedOptionIndex { get; set; }
    }
}
