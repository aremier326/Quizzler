using Microsoft.AspNetCore.Mvc;

namespace Quizzler.Mvc.PL.Controllers
{
    [Route("/")]
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Frontend")]
        public IActionResult Frontend()
        {
            return View();
        }

        [Route("/Backend")]
        public IActionResult BackEnd()
        {
            return View();
        }

        [Route("/QA")]
        public IActionResult QA()
        {
            return View();
        }
    }


}
