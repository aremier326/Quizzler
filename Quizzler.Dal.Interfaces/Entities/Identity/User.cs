using Microsoft.AspNetCore.Identity;
using Quizzler.Dal.Interfaces.Entities;

namespace Quizzler.Dal.Interfaces.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public IEnumerable<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
