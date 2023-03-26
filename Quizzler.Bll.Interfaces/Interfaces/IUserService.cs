using Quizzler.Bll.Interfaces.Entities;
using Quizzler.Bll.Interfaces.Entities.BllModels;
using Quizzler.Dal.Interfaces.Entities;
using Quizzler.Dal.Interfaces.Entities.Identity;

namespace Quizzler.Bll.Interfaces.Interfaces
{
    public interface IUserService
    {
        ValueTask<ErrorModel> CreateAsync(BllUserModel userModel);
        ValueTask<ErrorModel> LoginAsync(BllUserModel userModel);
        Task SignOutAsync();
        ValueTask<IEnumerable<Quiz>> GetQuizzesAsync(string userId);
        ValueTask<ErrorModel> AddQuizAsync(string userId, Quiz quiz);
        ValueTask<Quiz> GetLastQuizAsync(string userId);
    }
}
