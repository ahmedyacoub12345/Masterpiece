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
        [HttpGet("GetDoctorBooking/{doctorId}")]
        public IActionResult GetDoctorBooking(int doctorId)
        {
            var bookings = _db.Bookings.Where(b => b.DoctorId == doctorId).ToList();
            var doctor = _db.Doctors
                .Where(d => d.DoctorId == doctorId)
                .Select(d => new
                {
                    DoctorId = d.DoctorId,
                    Name = d.Name,
                    Email = d.Email,
                    Phone = d.Phone,
                    Qualifications = d.Qualifications,
                    ClinicAddress = d.ClinicAddress
                })
                .FirstOrDefault();

            if (doctor == null)
                return NotFound("Doctor not found.");

            var response = new
            {
                Doctor = doctor,
                Bookings = bookings
            };

            return Ok(response);
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
                                     && b.Time == booking.Time);

            if (existingBooking != null)
                return Conflict("The selected time is already booked for this doctor.");

            var book = new Booking
            {
                UserId = booking.UserId,
                DoctorId = booking.DoctorId,
                Time = booking.Time,
                BookingDate = DateTime.Now,
                PaymentStatus = "Pendding"
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
                bookingDate = DateTime.Now,
                paymentStatus = book.PaymentStatus,

            };

            return CreatedAtAction(nameof(CreateBooking), new { id = book.BookingId }, response);
        }
        [HttpGet("GetTime/{id}")]
        public IActionResult getTime(int id)
        {
            var data = _db.Bookings.Where(a => a.DoctorId == id).Select(a => a.Time).ToList();
            return Ok(data);

        }
        [HttpGet("GetAllUsers/{doctorId}")]
        public IActionResult GetAllUsers(int doctorId)
        {
            var data = _db.Bookings
                .Where(booking => booking.DoctorId == doctorId)
                .Select(booking => new
                {
                    UserID = booking.UserId,
                    Username = booking.User.Username,
                    PhoneNumber = booking.User.PhoneNumber,
                    Email = booking.User.Email,
                    Address = booking.User.Address,
                    UserImage = booking.User.UserImage,
                })
                .Distinct()
                .ToList();

            return Ok(data);
        }
        [HttpPost("BookAppointment")]
        public IActionResult BookAnAppointment([FromBody] BookingModel bookingData)
        {
            if (bookingData == null)
            {
                return BadRequest("Invalid booking data");
            }

            // Create a new Booking entity from the BookingModel
            var booking = new Booking
            {
                UserId = bookingData.UserId,
                DoctorId = bookingData.DoctorId,
                Time = bookingData.Time,
                BookingDate = DateTime.Parse(bookingData.Date), // Assuming booking date is passed as string in ISO 8601 format
                PaymentStatus = bookingData.PaymentStatus
            };

            try
            {
                // Add the booking data to the database
                _db.Bookings.Add(booking);

                // Save changes to the database
                _db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                // Log the error (uncomment ex variable name and write a log if needed)
                return StatusCode(500, "Internal server error while saving booking data.");
            }

            return Ok(new { message = "Appointment booked successfully!" });
        }


        public class BookingModel
        {
            public int UserId { get; set; }
            public int DoctorId { get; set; }
            public TimeOnly Time { get; set; }  // Change string to TimeOnly
            public string Date { get; set; }
            public string PaymentStatus { get; set; }
        }

    }
}

