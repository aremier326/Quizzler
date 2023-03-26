using Quizzler.Dal.Interfaces.Entities.Base;
using Quizzler.Dal.Interfaces.Enums;

namespace Quizzler.Dal.Interfaces.Entities
{
    public class Test : BaseEntity
    {
        public string Question { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public string Answer3 { get; set; }

        public string Answer4 { get; set; }

        public string CorrectAnswer { get; set; }

        public int TestType { get; set; }
    }
}
