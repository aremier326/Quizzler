using Quizzler.Dal.Interfaces.Entities.Base;
using Quizzler.Dal.Interfaces.Enums;

namespace Quizzler.Dal.Interfaces.Entities
{
    public class Quiz : BaseEntity
    {
        public Quiz() { }

        public string Description { get; set; } = "Just some test";

        public IEnumerable<ActiveTest> ActiveTests { get; set; } = new List<ActiveTest>();

        public decimal Result { get; set; }

        public TestTypeEnum QuizType { get; set; }
    }
}
