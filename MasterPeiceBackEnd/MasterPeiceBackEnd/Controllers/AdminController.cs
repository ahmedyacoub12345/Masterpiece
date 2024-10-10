using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterPeiceBackEnd.TokenReaderNS;
using Microsoft.AspNetCore.Authorization;
namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        private readonly TokenGenerator _tokenGenerator;
        private readonly MasterPeiceBackEnd.TokenReaderNS.TokenReader _tokenReader;

        public AdminController(MedicalAppContext db, TokenGenerator tokenGenerator, MasterPeiceBackEnd.TokenReaderNS.TokenReader tokenReader)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
            _tokenReader = tokenReader;
        }
        
        [HttpGet("GetAllUsersAndAdmins")]
        public IActionResult GetAllUsersAndAdmins()
        {
            
            var usersWithAdmins = _db.Users
                .Join(
                    _db.Admins,              
                    user => user.UserID,       
                    admin => admin.UserId,     
                    (user, admin) => new       
                    {
                        user.Username,         
                        user.Email,            
                        user.PhoneNumber,      
                        user.Address,          
                        AdminId = admin.AdminId,  
                    }
                )
                .ToList();

            return Ok(usersWithAdmins);
        }
        
        [HttpPost("LoginAdmin")]
        public IActionResult LoginAdmin([FromForm] AdminLoginDTO admin)
        {
            var adminData = _db.Admins.Include(a => a.User).FirstOrDefault(a => a.User.Email == admin.Email);

            if (adminData == null || !PasswordHash.verifyPassword(admin.Password, adminData.User.PasswordHash, adminData.User.PasswordSalt))
            {
                return Unauthorized();
            }

            var token = _tokenGenerator.GenerateToken(adminData.User.Username);

            var usersWithAdmins = _db.Users
                .Join(
                    _db.Admins,
                    user => user.UserID,
                    admin => admin.UserId,
                    (user, admin) => new
                    {
                        user.Username,
                        user.Email,
                        user.PhoneNumber,
                        user.Address,
                        AdminId = admin.AdminId,
                    }
                )
                .ToList();

            var response = new
            {
                Token = token,
                UsersWithAdmins = usersWithAdmins
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet("getCurrentUserInfo")]
        public IActionResult GetCurrentUser()
        {
            var user = GetUser();
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        private User? GetUser()
        {


            var authHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                Console.WriteLine("Authorization header is missing or malformed.");
                return null;
            }

            var token = authHeader.Split(' ')[1];

            var principal = _tokenReader.ValidateToken(token);
            if (principal?.Identity?.Name == null)
            {
                Console.WriteLine("Invalid token or missing claims.");
                return null;
            }
            return _db.Users.
                Include(u => u.Admins).
                Include(u => u.Admins).FirstOrDefault(u => u.Username == principal.Identity.Name);
        }

    }
}
