using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using ZPZP.Data;
using ZPZP.Models;

namespace ZPZP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUsers([FromForm] string userName, [FromForm] string userPassword, [FromForm] string userDomain, [FromForm] string userLevel, [FromForm] string name, [FromForm] string surname, [FromForm] string email, [FromForm] IFormFile imageFile, [FromForm] int phoneNumber)
        {
            Users user = new Users
            {
                UserName = userName,
                UserPassword = userPassword,
                UserLevel = userLevel,
                Name = name,
                Surname = surname,
                Email = email,
                PhoneNumber = phoneNumber,
            };

            if (imageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    user.Image = memoryStream.ToArray();
                }
            }

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}