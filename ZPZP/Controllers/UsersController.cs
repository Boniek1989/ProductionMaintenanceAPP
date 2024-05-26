using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System.Net;
using ZPZP.Data;
using ZPZP.Models;

namespace ZPZP.Controllers
{
    [Route("dashboard")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _appDbContext;
      

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
          
        }

        [Route("home")]
        [HttpPost]
        public IActionResult Authorization(string username, string pwd, string dropdown)
        {
            var admin = _appDbContext.Users.FirstOrDefault(u => u.UserName == username && u.UserPassword == pwd && u.UserLevel == "Kierownik");
            var worker = _appDbContext.Users.FirstOrDefault(u => u.UserName == username && u.UserPassword == pwd && u.UserLevel == "Pracownik");

            if (admin != null)
            {
                int ID = admin.ID;

                TempData["ID"] = admin.ID;
                ViewBag.Image = admin.Image;
                ViewBag.Username = username;
                ViewBag.UserLevel = "Kierownik";
                ViewBag.Category = dropdown;
                ViewBag.Name = admin.Name;
                ViewBag.Surname = admin.Surname;
                ViewBag.Email = admin.Email;
                ViewBag.PhoneNumber = admin.PhoneNumber;
                ViewBag.Password = admin.UserPassword;
                ViewBag.ID = admin.ID;   


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


        //  [HttpPost]
        //  public async Task<IActionResult> EditImage(IFormFile file, int userID, int ID)
        //  {

        //      var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.ID == userID);


        //    if (imageFile != null)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //       {
        //            await imageFile.CopyToAsync(memoryStream);
        //            user.Image = memoryStream.ToArray();
        //         }
        //     }

        //    _appDbContext.Users.Add(user);
        //     await _appDbContext.SaveChangesAsync();

        //     return Ok(user);
        //  }
        
        [Route("settings")]
        public IActionResult Settings ()
        {
            if (TempData["ID"] != null)
            {
                ViewBag.ID = TempData["ID"];
                return View("~/Views/Production/AdminSettings.cshtml");
            }
            else

            ViewBag.Message = "Sesja wygasła - wymagane ponowne logowanie";
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        [Route("settings-password")]
        public async Task<IActionResult> EditPassword(string newPwd, int userID)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.ID == userID);

            user.UserPassword = newPwd;

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();

            ViewBag.Message = "Pomyślna zmiana hasła";
            return View("~/Views/Home/Index.cshtml");
        }
        [HttpPost]
        [Route("settings-image")]
        public async Task<IActionResult> EditImage(IFormFile file, int userID)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "No file selected";
                return View("~/Views/Home/Index.cshtml");
            }

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.ID == userID);
            if (user == null)
            {
                ViewBag.Message = "User not found";
                return View("~/Views/Home/Index.cshtml");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                user.Image = memoryStream.ToArray();
            }

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();

            ViewBag.Message = "Image updated successfully";
            return View("~/Views/Home/Index.cshtml");
        }
    }

}

