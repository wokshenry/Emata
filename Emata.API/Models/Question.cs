using Emata.API.Models.Shared;
using Emata.Shared.Shared.Enums;

namespace Emata.API.Models
{
    public class Question : Entity
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOptionIndex { get; set; }
        public string? Category { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
    }
}
