using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Diagnostics;
using ZPZP.Data;
using ZPZP.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZPZP.Controllers
{

    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;


        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        [Route("dashboard/production/manager/addproject")]
        public async Task<ActionResult<IEnumerable<Users>>> AddProject()
        {
            var productionEmployees = await _appDbContext.Users.Where(u => u.UserCategory == "produkcja" && u.UserLevel == "pracownik").ToListAsync();

            string formattedDateOnly = DateTime.Now.ToString("yyyy-MM-dd");

            ViewBag.ProductionEmployees = new SelectList(productionEmployees, "Id", "Surname");
            ViewBag.Timestamp = formattedDateOnly;

            return View("~/Views/Production/Manager/AddProject.cshtml");
        }
        [HttpPost]
        [Route("dashboard/production/manager/addproject-add")]
        public async Task<IActionResult> StoreProject([FromForm] string? serialNumber, string? name, [FromForm] IFormFile file, string? dropdownOne, string? dropdownTwo, string? dropdownThree, string? Surname, DateTime date, DateTime date2, string? status)
        {
            //var EmployeeOne = _appDbContext.Users.FirstOrDefault(u => u.UserCategory == "produkcja" && u.UserLevel == "pracownik");

            //ViewBag.ProductionEmployees = new SelectList(productionEmployees, "ID", "Name");

            Products product = new Products
            {
                SerialNumber = serialNumber,
                Name = name,
                Author = Surname,
                ProductionWorkerAssigned1 = dropdownOne,
                ProductionWorkerAssigned2 = dropdownTwo,
                ProductionWorkerAssigned3 = dropdownThree,
                ProjectDate = date2,
                ProjectDateStart = date,
                Status = status,

            };
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    product.ProductionDocumentation = memoryStream.ToArray();
                }
            }

            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();

            ViewBag.Message = "Projekt o nazwie " + product.Name + " o numerze seryjnym " + product.SerialNumber + " został zlecony do produkcji.";

            return View("~/Views/Production/Manager/AdminStatus.cshtml");
        }
        [HttpGet]
        [Route("dashboard/production/manager/showprojects")]
        public IActionResult ShowAll() 
        {
            var projects = _appDbContext.Products.ToList();
            ViewBag.Message = "Brak rekordów w bazie danych, sprawdź pisownię.";


            return View("~/Views/Production/Manager/ShowProjects.cshtml" , projects);
        }
        [HttpPost]
        [Route("/dashboard/production/manager/find-serial")]
        public IActionResult FindSerial(string serial)
        {

            var projects = _appDbContext.Products.Where(u => u.SerialNumber == serial).ToList();
            ViewBag.Message = "Brak rekordów w bazie danych, sprawdź pisownię.";

            return View("~/Views/Production/Manager/ShowProjects.cshtml", projects);
        }
        [HttpPost]
        [Route("/dashboard/production/manager/find-name")]
        public IActionResult FindName(string name)
        {

            var projects = _appDbContext.Products.Where(u => u.Name == name).ToList();
            ViewBag.Message = "Brak rekordów w bazie danych, sprawdź pisownię.";

            return View("~/Views/Production/Manager/ShowProjects.cshtml", projects);
        }
        [HttpPost]
        [Route("/dashboard/production/manager/find-description")]
        public IActionResult FindDescription(string description)
        {

            var projects = _appDbContext.Products.Where(u => u.DescriptionProduction == description).ToList();
            ViewBag.Message = "Brak rekordów w bazie danych, sprawdź pisownię.";

            return View("~/Views/Production/Manager/ShowProjects.cshtml", projects);
        }
        [HttpPost]
        [Route("/dashboard/production/manager/find-person-name")]
        public IActionResult FindPersonName(string personName)
        {

            var projects = _appDbContext.Products.Where(u => u.ProductionWorkerAssigned1 == personName || u.ProductionWorkerAssigned2 == personName || u.ProductionWorkerAssigned3 == personName).ToList();
            ViewBag.Message = "Brak rekordów w bazie danych, sprawdź pisownię.";

            return View("~/Views/Production/Manager/ShowProjects.cshtml", projects);
        }
    }
}
