using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly MedicalAppContext _db;

        public AppointmentsController(MedicalAppContext db)
        {
            _db = db;
        }

        //[HttpGet("AllAppointment")]
        //public IActionResult Get()
        //{
        //    var data = _db.Appointments.ToList();
        //    return Ok(data);
        //}

        //// Get all appointments
        //[HttpGet("GetAllAppointments")]
        //public IActionResult GetAllAppointments()
        //{
        //    try
        //    {
        //        var data = _db.Appointments.Include(a => a.Doctor).ToList();
        //        if (data == null || !data.Any())
        //        {
        //            return NotFound("No appointments found.");
        //        }

        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching appointments.");
        //    }
        //}

        //// Get appointment by ID
        //[HttpGet("GetAppointment/{id}")]
        //public IActionResult GetAppointment(int id)
        //{
        //    try
        //    {
        //        var appointment = _db.Appointments.Include(a => a.Doctor).FirstOrDefault(a => a.AppointmentId == id);
        //        if (appointment == null)
        //        {
        //            return NotFound("Appointment not found.");
        //        }

        //        return Ok(appointment);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the appointment.");
        //    }
        //}

        //// Add a new appointment (Doctor adds) - using AddAppointmentDTO
        //[HttpPost("AddAppointment")]
        //public IActionResult AddAppointment([FromForm] AddAppointmentDTO dto)
        //{
        //    try
        //    {
        //        var appointment = new Appointment
        //        {
        //            DoctorId = dto.DoctorId,
        //            Date = dto.Date,
        //            Time = dto.Time,
        //            Available = dto.Available
        //        };

        //        _db.Appointments.Add(appointment);
        //        _db.SaveChanges();

        //        return Ok("Appointment added successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the appointment.");
        //    }
        //}

        //// Update appointment details - using UpdateAppointmentDTO
        //[HttpPut("UpdateAppointment/{id}")]
        //public IActionResult UpdateAppointment(int id, [FromForm] UpdateAppointmentDTO dto)
        //{
        //    try
        //    {
        //        var appointment = _db.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        //        if (appointment == null)
        //        {
        //            return NotFound("Appointment not found.");
        //        }

        //        appointment.Date = dto.Date;
        //        appointment.Time = dto.Time;
        //        appointment.Available = dto.Available;

        //        _db.SaveChanges();

        //        return Ok("Appointment updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the appointment.");
        //    }
        //}

        //// Book an appointment
        //[HttpPost("BookAppointment")]
        //public IActionResult BookAppointment([FromForm] int appointmentId, [FromForm] int userId)
        //{
        //    try
        //    {
        //        var appointment = _db.Appointments.Include(a => a.Doctor).ThenInclude(d => d.Availabilities)
        //            .FirstOrDefault(a => a.AppointmentId == appointmentId);

        //        if (appointment == null)
        //        {
        //            return NotFound("Appointment not found.");
        //        }

        //        // Check if the appointment date and time falls within the doctor's availability
        //        var isAvailable = appointment.Doctor.Availabilities.Any(a =>
        //            a.Date.Date == appointment.Date.Date &&
        //            appointment.Time >= a.StartTime &&
        //            appointment.Time <= a.EndTime);

        //        if (!isAvailable)
        //        {
        //            return BadRequest("The appointment time is not available.");
        //        }

        //        if (!appointment.Available)
        //        {
        //            return BadRequest("The appointment is not available.");
        //        }

        //        var booking = new Booking
        //        {
        //            //AppointmentId = appointmentId,
        //            UserId = userId,
        //            BookingDate = DateTime.Now
        //        };

        //        appointment.Available = false; // Once booked, mark the appointment as unavailable
        //        _db.Bookings.Add(booking);
        //        _db.SaveChanges();

        //        return Ok("Appointment booked successfully.");
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while booking the appointment.");
        //    }
        //}

        //// Check if an appointment is available
        //[HttpGet("CheckAvailability/{appointmentId}")]
        //public IActionResult CheckAvailability(int appointmentId)
        //{
        //    try
        //    {
        //        var appointment = _db.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

        //        if (appointment == null)
        //        {
        //            return NotFound("Appointment not found.");
        //        }

        //        if (appointment.Available)
        //        {
        //            return Ok("The appointment is available.");
        //        }

        //        return Ok("The appointment is not available.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while checking availability.");
        //    }
        //}
        [HttpGet("ProblemSolving")]
        public IActionResult ProblemSolving()
        {
            int[] x = { 5, 7, 5, 2, 4, 3, 1, 0 };
            Array.Sort(x);
            foreach (int i in x)
            {
                x[i] = i;
                return Ok(i);
            }
            return Ok();
        }
        [HttpGet("ProblemSolving1")]
        public IActionResult ProblemSolving1()
        {
            int[] x = { 5, 7, 5, 2, 4, 3, 1, 0 };
            int s = 0;
            for (int i = 0; i < x.Length; i++)
            {
                s += x[i];
            }
            return Ok(s);
        }
        [HttpGet("ProblemSolving2")]
        public IActionResult ProblemSolving2(int n)
        {
            int sum = 0;

            while (n > 0)
            {
                sum += n%10;
                n /=10;
            }

            return Ok(sum); 
        }
        [HttpGet("ProblemSolving3")]
        public IActionResult ProblemSolving3(string x)
        {
            int sum = 0;
            for (int i = 0; i <= x.Length; i++){
                sum += (int)x[i];
                return Ok(sum);
            }
            return Ok();
        }
    }
}
