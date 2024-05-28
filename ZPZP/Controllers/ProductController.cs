﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("dashboard/addproject")]
        public async Task<ActionResult<IEnumerable<Users>>> AddProject()
        {
            var productionEmployees = await _appDbContext.Users.Where(u => u.UserCategory == "produkcja" && u.UserLevel == "pracownik").ToListAsync();

            string formattedDateOnly = DateTime.Now.ToString("yyyy-MM-dd");

            ViewBag.ProductionEmployees = new SelectList(productionEmployees, "ID", "Surname");
            ViewBag.Timestamp = formattedDateOnly;

            return View("~/Views/Production/AddProject.cshtml");
        }
        [HttpPost]
        [Route("dashboard/addproject-add")]
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

            return View("~/Views/Production/AdminStatus.cshtml");
        }
        [HttpGet]
        [Route("dashboard/showprojects")]
        public IActionResult ShowAll() 
        {
            var projects = _appDbContext.Products.ToList();
            


            return View("~/Views/Production/ShowProjects.cshtml" , projects);
        }
    }
}
