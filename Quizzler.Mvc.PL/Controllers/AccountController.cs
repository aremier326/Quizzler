using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quizzler.Bll.Interfaces.Entities;
using Quizzler.Bll.Interfaces.Entities.BllModels;
using Quizzler.Bll.Interfaces.Interfaces;
using Quizzler.Dal.Interfaces.Entities.Identity;
using Quizzler.Mvc.PL.Models;

namespace Quizzler.Mvc.PL.Controllers;

[Route("/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Error()
    {
        ViewBag.Errors = TempData["Errors"];
        return View();
    }

    [HttpGet]
    public IActionResult Successful(BllUserModel model)
    {
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Register(UserInputModel userInputModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userInputModel);
        }

        BllUserModel userModel = new BllUserModel()
        {
            Email = userInputModel.Email,
            Password = userInputModel.Password
        };

        var errModel = await _userService.CreateAsync(userModel);
        if (!errModel.IsSuccess)
        {
            AddModelStateErrors(errModel);
            return View(userInputModel);
        }

        return RedirectToAction(nameof(Successful), userModel);
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

        var errModel = await _userService.LoginAsync(new BllUserModel()
        {
            Email = userInputModel.Email,
            Password = userInputModel.Password
        });

        if (!errModel.IsSuccess)
        {
            AddModelStateErrors(errModel);
            return View(userInputModel);
        }

        return RedirectToAction("Index", "Main");
    }

    public async ValueTask<IActionResult> LogOut()
    {
        await _userService.SignOutAsync();
        return RedirectToAction("Index", "Main");
    }

    private void AddModelStateErrors(ErrorModel errorModel)
    {
        foreach (var item in errorModel.ErrorDetails)
        {
            ModelState.AddModelError(string.Empty, item.ErrorMessage);
        }
    }

}