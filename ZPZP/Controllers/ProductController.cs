using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        [Route("dashboard/addproject")]
        public async Task<ActionResult<IEnumerable<Users>>> AddProject()
        {
            var productionEmployees = await _appDbContext.Users.Where(u => u.UserCategory == "produkcja" && u.UserLevel =="pracownik").ToListAsync();

            ViewBag.ProductionEmployees = new SelectList(productionEmployees,"ID","Name");
                       
            return View("~/Views/Production/AddProject.cshtml");
        }
    }
}
