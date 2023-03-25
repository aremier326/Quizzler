using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quizzler.Dal.Interfaces.Entities.Identity;
using Quizzler.Mvc.PL.Models;

namespace Quizzler.Mvc.PL.Controllers;

[Route("/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Successful()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Register(UserInputModel userInputModel)
    {
        if (!ModelState.IsValid) return View(userInputModel);

        var user = new User
        {
            Email = userInputModel.Email,
            UserName = userInputModel.UserName
        };
        var result = await _userManager.CreateAsync(user, userInputModel.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            return View(userInputModel);
        }

        return RedirectToAction(nameof(Successful));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Login(UserInputModel userInputModel)
    {
        if (!ModelState.IsValid) return View(userInputModel);

        User user = await _userManager.FindByEmailAsync(userInputModel.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "SignIn failure: wrong email or password");
            return View(userInputModel);
        }

        await _signInManager.SignOutAsync();

        var result =
            await _signInManager.PasswordSignInAsync(user, userInputModel.Password, false, true);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "SignIn failure: wrong email or password");
            return View(userInputModel);
        }

        return RedirectToAction("Index", "Main");
    }

    public async ValueTask<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Main");
    }

}