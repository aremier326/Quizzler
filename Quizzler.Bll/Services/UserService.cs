using Microsoft.AspNetCore.Identity;
using Quizzler.Bll.Interfaces.Entities;
using Quizzler.Bll.Interfaces.Entities.BllModels;
using Quizzler.Bll.Interfaces.Interfaces;
using Quizzler.Dal.Interfaces.Entities.Identity;

namespace Quizzler.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
    }
}
