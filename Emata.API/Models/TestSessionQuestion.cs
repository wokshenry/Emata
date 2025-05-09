using Emata.API.Models.Shared;

namespace Emata.API.Models
{
    public class TestSessionQuestion : Entity
    {
        // Foreign keys
        public Guid SessionId { get; set; }
        public Guid QuestionId { get; set; }

        // Order of question in the test
        public int OrderIndex { get; set; }

        // Navigation properties
        public virtual TestSession? Session { get; set; }
        public virtual Question? Question { get; set; }
    }
}
