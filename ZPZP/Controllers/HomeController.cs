using Microsoft.AspNetCore.Mvc;

namespace ZPZP.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
