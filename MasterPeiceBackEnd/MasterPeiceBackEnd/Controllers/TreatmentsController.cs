using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        public TreatmentsController(MedicalAppContext db)
        {
            _db = db;
        }
        [HttpGet("GetAllTratment")]
        public IActionResult GetAllTratment()
        {
            var data = _db.Treatments.ToList();
            return Ok(data);
        }
        [HttpPost("AddTreatment")]
        public IActionResult AddTreatment([FromForm] AddTreatmentDTO treatment)
        {
            var data = new Treatment
            {
                DoctorId = treatment.DoctorId,
                UserId = treatment.UserId,
                Description = treatment.Description,
                Date = DateTime.Now,
            };
            _db.Treatments.Add(data);
            _db.SaveChanges();
            return Ok(data); 
        }
        [HttpGet("GetDiagnosesByUserId/{id}")]
        public IActionResult GetDiagnosesByUserId(int id) 
        {
            var data = _db.Treatments.Find(id);
            return Ok(data);
        }
        [HttpGet("GetTreatmentsByUserId/{id}")]
        public IActionResult GetTreatmentsByUserId(int id)
        {
            var data = _db.Treatments
                .Where(t => t.UserId == id)
                .Select(treatment => new
                {
                    TreatmentId = treatment.TreatmentId,
                    Description = treatment.Description,
                    Date = treatment.Date,
                    DoctorName = treatment.Doctor.Name, 
                    UserName = $"{treatment.User.FirstName} {treatment.User.LastName}"
                })
                .ToList();

            return Ok(data);
        }

    }
}
