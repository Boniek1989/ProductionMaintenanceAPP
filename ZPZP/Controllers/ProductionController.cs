using Microsoft.AspNetCore.Mvc;

namespace ZPZP.Controllers
{
    public class ProductionController : Controller
    {
      //  public IActionResult Settings() 
     //   { 
     //       return View(); 
     //   }

        [Route("dashboard/settings")]
        public IActionResult Settings(string userImage, string userPassword)
        {
            return Json(new { success = true, message = "Data received" });
        }
    }
}
