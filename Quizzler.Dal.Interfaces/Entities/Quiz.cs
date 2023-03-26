using Quizzler.Dal.Interfaces.Entities.Base;

namespace Quizzler.Dal.Interfaces.Entities
{
    public class Quiz : BaseEntity
    {
        public Quiz() { }

        public Quiz(int type)
        {
            QuizType = type;
        }

        public string Description { get; set; } = "Just some test";

        public IEnumerable<ActiveTest> ActiveTests { get; set; } = new List<ActiveTest>();

        public double Result { get; set; }

        public int QuizType { get; set; }
    }
}
