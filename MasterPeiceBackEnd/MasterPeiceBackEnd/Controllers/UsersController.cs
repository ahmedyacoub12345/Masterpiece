using DinkToPdf.Contracts;
using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static MasterPeiceBackEnd.Shared.ImageSaver;
using Microsoft.AspNetCore.Authorization;
using MasterPeiceBackEnd.TokenReaderNS;
using Microsoft.IdentityModel.Tokens;


namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        private readonly TokenGenerator _tokenGenerator;
        private readonly EmailService _emailService;
        private readonly IConfiguration _config;
        private readonly IConverter _converter;
        private readonly MasterPeiceBackEnd.TokenReaderNS.TokenReader _tokenReader;

        public UsersController(MedicalAppContext db, TokenGenerator tokenGenerator, MasterPeiceBackEnd.TokenReaderNS.TokenReader tokenReader, EmailService emailService, IConfiguration config, IConverter converter)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
            _emailService = emailService;
            _config = config;
            _converter = converter;
            _tokenReader = tokenReader;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var data = _db.Users.ToList();
            return Ok(data);
        }
        [HttpGet("ShowUserByID/{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var data = _db.Users.Find(id);
            return Ok(data);
        }
        [HttpPost("RegisterUsers")]
        public IActionResult Register([FromForm] UserRegisterDTO user)
        {
            byte[] hash;
            byte[] salt;
            PasswordHash.Hasher(user.Password, out hash, out salt);
            var data = new User
            {
                FirstName = user.FirstName,
                MidName = user.MidName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                UserImage = SaveImage(user.UserImage),
                PasswordHash = hash,
                PasswordSalt = salt,
            };
            var token = _tokenGenerator.GenerateToken(data.Username);
            var response = new
            {
                Token = token,
                User = data
            };
            _db.Users.Add(data);
            _db.SaveChanges();
            return Ok(response);
        }
        [HttpPost("LoginUsers")]
        public IActionResult Login([FromForm] UserLoginDTO user)
        {
            var data = _db.Users.FirstOrDefault(x => x.Email == user.Email);
            if (data == null || !PasswordHash.verifyPassword(user.Password, data.PasswordHash, data.PasswordSalt))
            {
                return Unauthorized();
            }

            var token = _tokenGenerator.GenerateToken(data.Username);

            var response = new
            {
                Token = token,
                User = data
            };

            return Ok(response);
        }
        [HttpPut("UpdateUser/{id:int}")]
        public IActionResult UpdateUser(int id, [FromForm] UpdateUserDTO user)
        {

            var data = _db.Users.Find(id);

            if (data == null)
                return NotFound();

            if (user.UserName != null)
                data.FirstName = user.FirstName;
            if (user.LastName != null)
                data.LastName = user.LastName;
            if (user.UserName != null)
                data.Username = user.UserName;
            if (user?.Email != null)
                data.Email = user.Email;
            if (user?.PhoneNumber != null)
                data.PhoneNumber = user.PhoneNumber;
            if (user?.UserImage != null)
                data.UserImage = SaveImage(user.UserImage);


            _db.Users.Update(data);
            _db.SaveChanges();
            return Ok(user);
        }
        [HttpDelete("DeleteUser/{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _db.Users.Find(id);
            _db.Users.Remove(user);
            _db.SaveChanges();
            return Ok(user);
        }
        [HttpPut("ChangePassword/{id:int}")]
        public IActionResult ChangePassword(int id, [FromBody] ChangePasswordDTO user)
        {
            byte[] hash;
            byte[] salt;
            PasswordHash.Hasher(user.Password, out hash, out salt);
            var data = _db.Users.Find(id);
            if (data == null)
                return NotFound();
            data.Password = user.Password;
            data.PasswordHash = hash;
            data.PasswordSalt = salt;
            _db.Users.Update(data);
            _db.SaveChanges();
            return Ok(user);
        }
        [HttpGet("GetUserByEmail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email parameter is required.");
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromForm] EmailRequest request)
        {
            // Generate OTP
            var otp = OtpGenerator.GenerateOtp();
            var user = _db.Users.FirstOrDefault(x => x.Email == request.ToEmail);
            if (user == null) return NotFound();
            user.Password = otp;
            await _db.SaveChangesAsync();

            // Create email body including the OTP
            var emailBody = $"Hello Dear, Your MedicalApp OTP code for resetting your password is: {otp} Thank you.";
            const string subject = "send OTP";
            // Send email with OTP
            //await _emailService.SendEmailAsync(request.ToEmail, Subject, emailBody);
            Shared.EmailSender.SendEmail(request.ToEmail, subject, emailBody);

            return Ok(new { message = "Email sent successfully.", otp, user.UserID }); // Optionally return the OTP for testing
        }

        [HttpPost("GetOTP/{id}")]
        public IActionResult GetOtp([FromForm] OTPDTO request, int id)
        {
            var user = _db.Users.Find(id);
            if (user?.Password == request.OTP)
            {
                return Ok();
            }
            return BadRequest();
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


            // Check if Authorization header exists
            var authHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                Console.WriteLine("Authorization header is missing or malformed.");
                return null;
            }

            // Extract token from Authorization header
            var token = authHeader.Split(' ')[1];

            var principal = _tokenReader.ValidateToken(token);
            if (principal?.Identity?.Name == null)
            {
                Console.WriteLine("Invalid token or missing claims.");
                return null;
            }
            return _db.Users.
                Include(u => u.Doctors).
                ThenInclude(u => u.Availabilities).
                Include(u => u.Admins).FirstOrDefault(u => u.Username == principal.Identity.Name);

        }

    }
}