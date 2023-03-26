using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Quizzler.Bll.Interfaces.Interfaces;
using Quizzler.Dal.Interfaces.Entities;
using Quizzler.Mvc.PL.Utils;
using System;
using System.Linq;
using System.Security.Claims;

namespace Quizzler.Mvc.PL.Controllers
{
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public class MainController : Controller
    {
        private readonly IUserService _userService;
        private readonly IQuizService _quizService;
        private readonly ITestService _testService;

        public MainController(IUserService userService, IQuizService quizService, ITestService testService)
        {
            _userService = userService;
            _quizService = quizService;
            _testService = testService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/Shared/Error")]
        public IActionResult Error()
        {
            ViewBag.Errors = TempData["Errors"];
            return View();
        }

        [Route("/Frontend")]
        public async ValueTask<IActionResult> Frontend()
        {
            QuizPreparations(0, 5);

            return View();
        }

        [Route("/Backend")]
        public async ValueTask<IActionResult> BackEnd()
        {
            QuizPreparations(1, 5);

            return View();
        }

        [Route("/QA")]
        public async ValueTask<IActionResult> QA()
        {
            QuizPreparations(2, 5);

            return View();
        }

        [Route("/[controller]/Quiz")]
        public async ValueTask<IActionResult> Quiz()
        {
            Test test;

            if (TempData.Get<Queue<Test>>("Quiz") != null)
            {
                var queue = TempData.Get<Queue<Test>>("Quiz");
                
                if (queue.Count > 0)
                {
                    test = queue.Dequeue();

                    TempData.Put("Quiz", queue);
                    TempData.Keep();
                }
                else
                {
                    var completedQuiz = TempData.Get<Quiz>("ActiveQuiz");
                    await _quizService.GetResultAsync(completedQuiz);
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var errModel = await _userService.AddQuizAsync(userId, completedQuiz);

                    if (!errModel.IsSuccess)
                    {
                        TempData["Errors"] = errModel.ErrorDetails
                            .Select(e => e.ErrorMessage)
                            .ToList();
                        return RedirectToAction(nameof(Error));
                    }

                    return RedirectToAction("QuizResult");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            TempData.Keep();
            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async ValueTask<IActionResult> Quiz(int testId)
        {
            var test = await _testService.Get(testId);
            
            string selectedOption = Request.Form["SelectedOption"];

            if (selectedOption != null)
            {
                var completedQuiz = TempData.Get<Quiz>("ActiveQuiz");
                completedQuiz.ActiveTests = completedQuiz.ActiveTests.Append(
                    await _testService.CheckTest(test, selectedOption)
                    );
                TempData.Put("ActiveQuiz", completedQuiz);
            }

            TempData.Keep();

            return RedirectToAction("Quiz");
        }

        [Route("/[controller]/QuizResult")]
        public async ValueTask<IActionResult> QuizResult()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(await _userService.GetLastQuizAsync(userId));
        }

        public async void QuizPreparations(int category, int number)
        {
            var tests = await _quizService.GetTestsAsync(category, number);
            var queue = new Queue<Test>();

            foreach (var t in tests)
            {
                queue.Enqueue(t);
            }

            TempData.Put("ActiveQuiz", new Quiz(category));
            TempData.Put("Quiz", queue);
            TempData["QuizType"] = 2;
            TempData.Keep();
        }
    }


}
