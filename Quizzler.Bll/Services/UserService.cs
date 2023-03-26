using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizzler.Bll.Interfaces.Entities;
using Quizzler.Bll.Interfaces.Entities.BllModels;
using Quizzler.Bll.Interfaces.Interfaces;
using Quizzler.Dal.Data.DbContextData;
using Quizzler.Dal.Interfaces.Entities;
using Quizzler.Dal.Interfaces.Entities.Identity;
using System.Security.Cryptography.X509Certificates;

namespace Quizzler.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly MainDbContext _context;
        private readonly IQuizService _quizService;

        public UserService(SignInManager<User> signInManager, UserManager<User> userManager
                            , IQuizService quizService, MainDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _quizService = quizService;
            _context = context;
        }

        public async ValueTask<ErrorModel> CreateAsync(BllUserModel userModel)
        {
            var user = new User
            {
                Email = userModel.Email,
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                var errorModel = new ErrorModel()
                {
                    ErrorDetails = result.Errors.Select(er => new ErrorDetail()
                    {
                        ErrorMessage = er.Description
                    })
                };
                return errorModel;
            }

            return new ErrorModel();
        }

        public async ValueTask<ErrorModel> LoginAsync(BllUserModel userModel)
        {
            User user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user == null)
            {
                var errorModel = new ErrorModel();
                errorModel.ErrorDetails = new List<ErrorDetail>()
                {
                     new ErrorDetail()
                     {
                         ErrorMessage = "SignIn failure: Wrong login info!"
                     }
                };
                return errorModel;
            }

            await _signInManager.SignOutAsync();

            var result =
                await _signInManager.PasswordSignInAsync(user, userModel.Password, false, true);

            if (!result.Succeeded)
            {
                var errorModel = new ErrorModel();
                if (result.IsNotAllowed)
                {
                    errorModel.ErrorDetails = new List<ErrorDetail>()
                    {
                        new ErrorDetail()
                        {
                            ErrorMessage = "SignIn failure: Email is not confirmed"
                        }
                    };
                    return errorModel;
                }
                errorModel.ErrorDetails = new List<ErrorDetail>()
                {
                     new ErrorDetail()
                     {
                         ErrorMessage = "SignIn failure: Wrong login info!"
                     }
                };
                return errorModel;
            }

            return ErrorModel.CreateSuccess();
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }


        public async ValueTask<IEnumerable<Quiz>> GetQuizzesAsync(string userId)
        {
            var user = await _context.Users
                .Include(x =>  x.Quizzes)
                .ThenInclude(x => x.ActiveTests)
                .SingleOrDefaultAsync(x => x.Id == int.Parse(userId));

            if (user == null || user.Quizzes == null)
            {
                return null;
            }
            return user.Quizzes;
        }

        public async ValueTask<ErrorModel> AddQuizAsync(string userId, Quiz quiz)
        {
            var user = await _context.Users
                .Include(x => x.Quizzes)
                .ThenInclude(x => x.ActiveTests)
                .SingleOrDefaultAsync(x => x.Id == int.Parse(userId));

            if (user == null)
            {
                return null;
            }

            user.Quizzes = user.Quizzes.Append(await _quizService.CreateAsync(quiz)).ToList();

            await _userManager.UpdateAsync(user);

            return ErrorModel.CreateSuccess();
        }

        public async ValueTask<Quiz> GetLastQuizAsync(string userId)
        {
            return GetQuizzesAsync(userId).Result.Last();

        }
    }
}
