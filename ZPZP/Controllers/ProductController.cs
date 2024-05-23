using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace ZPZP.Controllers
{
    [Route("dashboard/addproject")]
    public class ProductController : Controller
    {
        public IActionResult AddProject()
        {
            return View("~/Views/Production/AddProject.cshtml");
        }
    }
}
