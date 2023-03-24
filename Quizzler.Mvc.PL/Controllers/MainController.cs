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
    }
}
