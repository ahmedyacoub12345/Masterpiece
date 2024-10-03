using MasterPeiceBackEnd.DTOs;
using MasterPeiceBackEnd.Models;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MasterPeiceBackEnd.Shared.EmailSender;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        public ContactController(MedicalAppContext db)
        {
            _db = db;
        }





        [HttpPost("AddMessage")]
        public IActionResult PostContactUs([FromForm] ContactRequest ContactRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var contactUs = new ContactUs
            {
                Name = ContactRequestDTO.Name,
                Email = ContactRequestDTO.Email,
                Message = ContactRequestDTO.Message,
            };

            _db.contactUs.Add(contactUs);
            _db.SaveChanges();
            SendEmail(ContactRequestDTO.Email, "Confirmation", "Thank you for contacting us. We will get back to you soon.");

            return Ok(new { message = "Contact form submitted successfully" });
        }


        [HttpGet("GetMessages")]
        public IActionResult GetContactMessages()
        {
            var messages = _db.contactUs
                              .OrderBy(m => m.SubmittedAt)
                              .ToList();

            return Ok(messages);
        }



        [HttpDelete("DeleteMessage/{id}")]
        public IActionResult DeleteMessage(int id)
        {

            var message = _db.contactUs.FirstOrDefault(m => m.MessageId == id);


            if (message == null)
            {
                return NotFound();
            }


            _db.contactUs.Remove(message);
            _db.SaveChanges();


            return Ok(new { message = "Message deleted successfully." });
        }
    }
}
