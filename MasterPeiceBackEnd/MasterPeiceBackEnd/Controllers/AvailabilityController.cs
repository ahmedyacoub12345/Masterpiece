using MasterPeiceBackEnd.DTOs;
using MasterPeiceBackEnd.Models;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly MedicalAppContext _db;

        public AvailabilityController(MedicalAppContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllAvailabilityDateByDoctorId/{id}")]
        public IActionResult GetAllAvailabilityDate(int id)
        {
            var available = _db.Doctors.Find(id);

            if (available == null)
            {
                return NotFound("Doctor not found.");
            }

            var data = _db.Availabilities
                .Where(u => u.DoctorId == available.DoctorId)
                .Select(d => new
                {
                    d.DoctorId,
                    d.AvailabilityId,
                    d.Date,
                    StartTime = d.StartTime.ToString(),
                    EndTime = d.EndTime.ToString()
                })
                .ToList();

            return Ok(data);
        }



        [HttpPost("AddAvailability")]
        public IActionResult AddAvailability([FromForm] AddAvailabilityDTO dto)
        {
            try
            {
                // Check if the doctor exists
                var doctorExists = _db.Doctors.Any(d => d.DoctorId == dto.DoctorId);
                if (!doctorExists)
                {
                    return NotFound("Doctor not found.");
                }

                var availability = new Availability
                {
                    DoctorId = dto.DoctorId,
                    Date = dto.Date,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime
                };

                _db.Availabilities.Add(availability);
                _db.SaveChanges();

                return Ok("Availability added successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        // Get availability for a doctor
        [HttpGet("GetAvailability/{doctorId}")]
        public IActionResult GetAvailability(int doctorId)
        {
            try
            {
                var availabilities = _db.Availabilities.Where(a => a.DoctorId == doctorId).ToList();
                return Ok(availabilities);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching availability.");
            }
        }

        // Update availability
        [HttpPut("UpdateAvailability/{id}")]
        public IActionResult UpdateAvailability(int id, [FromForm] UpdateAvailabilityDTO dto)
        {
            try
            {
                var availability = _db.Availabilities.FirstOrDefault(a => a.AvailabilityId == id);
                if (availability == null)
                {
                    return NotFound("Availability not found.");
                }

                availability.Date = dto.Date;
                availability.StartTime = dto.StartTime;
                availability.EndTime = dto.EndTime;

                _db.SaveChanges();
                return Ok("Availability updated successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating availability.");
            }
        }

        // Delete availability
        [HttpDelete("DeleteAvailability/{id}")]
        public IActionResult DeleteAvailability(int id)
        {
            try
            {
                var availability = _db.Availabilities.FirstOrDefault(a => a.AvailabilityId == id);
                if (availability == null)
                {
                    return NotFound("Availability not found.");
                }

                _db.Availabilities.Remove(availability);
                _db.SaveChanges();
                return Ok("Availability deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting availability.");
            }
        }
    }
}
