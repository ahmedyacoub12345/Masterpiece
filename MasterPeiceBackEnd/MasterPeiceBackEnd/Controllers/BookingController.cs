using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly MedicalAppContext _db; 

        public BookingController(MedicalAppContext db)
        {
            _db = db;
        }
        [HttpGet("GetAllBookings")]
        public IActionResult GetAllBookings() 
        {
            var data = _db.Bookings.ToList();
            return Ok(data);
        }

        [HttpPost("BookAnAppointment")]
        public IActionResult CreateBooking([FromForm] RequestBookingDTO booking)
        {
            if (booking == null)
                return BadRequest("Booking information is required.");

            var doctor = _db.Doctors
                .Include(d => d.Comments) // Include comments
                .FirstOrDefault(d => d.DoctorId == booking.DoctorId);

            if (doctor == null)
                return NotFound("Doctor not found.");

            var existingBooking = _db.Bookings
                .FirstOrDefault(b => b.DoctorId == booking.DoctorId
                                     && b.BookingDate.Date == booking.BookingDate.Date
                                     && b.Time == booking.Time);

            if (existingBooking != null)
                return Conflict("The selected time is already booked for this doctor.");

            var book = new Booking
            {
                UserId = booking.UserId,
                DoctorId = booking.DoctorId,
                Time = booking.Time,
                BookingDate = booking.BookingDate,
                PaymentStatus = booking.PaymentStatus
            };

            _db.Bookings.Add(book);
            _db.SaveChanges();

            var user = _db.Users.FirstOrDefault(u => u.UserID == booking.UserId);
            if (user == null)
                return NotFound("User not found.");

            var response = new
            {
                bookingId = book.BookingId,
                userId = user.UserID,
                userName = $"{user.FirstName} {user.LastName}",
                userEmail = user.Email,
                userPhoneNumber = user.PhoneNumber,
                doctorId = book.DoctorId,
                doctorName = doctor.Name,
                doctorEmail = doctor.Email,
                doctorQualifications = doctor.Qualifications,
                doctorClinicAddress = doctor.ClinicAddress,
                doctorPhone = doctor.Phone,
                time = book.Time,
                bookingDate = book.BookingDate,
                paymentStatus = book.PaymentStatus,
                
            };

            return CreatedAtAction(nameof(CreateBooking), new { id = book.BookingId }, response);
        }


    }
}
