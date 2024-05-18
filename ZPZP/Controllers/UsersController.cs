using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZPZP.Data;
using ZPZP.Models;

namespace ZPZP.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [Route("/dashboard")]
        [HttpPost]
        public IActionResult Authorization(string username, string pwd, string dropdown)
        {
            var admin = _appDbContext.Users.FirstOrDefault(u => u.UserName == username && u.UserPassword == pwd && u.UserLevel == "Kierownik");
            var worker = _appDbContext.Users.FirstOrDefault(u => u.UserName == username && u.UserPassword == pwd && u.UserLevel == "Pracownik");

            if (admin != null)
            {
                ViewBag.Image = admin.Image;
                ViewBag.Username = username;
                ViewBag.UserLevel = "Kierownik";
                ViewBag.Category = dropdown;
                ViewBag.Name = admin.Name;
                ViewBag.Surname = admin.Surname;
                ViewBag.Email = admin.Email;
                ViewBag.PhoneNumber = admin.PhoneNumber;


                switch (dropdown)
                {
                    case "produkcja":
                        return View("~/Views/Production/AdminDashboard.cshtml");
                    case "jakość":
                        return View("~/Views/Quality/AdminDashboard.cshtml");
                    case "logistyka":
                        return View("~/Views/Logistics/AdminDashboard.cshtml");
                    default:
                        return RedirectToAction("Index");
                }
            }
            if (worker != null)
            {
                ViewBag.Username = username;
                ViewBag.UserLevel = "Pracownik";

                switch (dropdown)
                {
                    case "produkcja":
                        return View("~/Views/Production/WorkerDashboard.cshtml");
                    case "jakość":
                        return View("~/Views/Quality/WorkerDashboard.cshtml");
                    case "logistyka":
                        return View("~/Views/Logistics/WorkerDashboard.cshtml");
                    default:
                        return RedirectToAction("Index");
                }
            }
            else
            ViewBag.ErrorMessage = "Nieprawidłowe dane logowania lub wybrany dział.";
            return View("~/Views/Home/Index.cshtml");
        }

    }
}

