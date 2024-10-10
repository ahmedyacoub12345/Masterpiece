using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class diagnosesController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        public diagnosesController(MedicalAppContext db)
        {
            _db = db;
        }
        [HttpGet("GetAllDiagnoses")]
        public IActionResult GetAllDiagnoses()
        {
            var data = _db.Diagnoses.ToList();
            return Ok(data);
        }
        [HttpPost("AddDiagnoses")]
        public IActionResult AddDiagnoses([FromForm] AddDiagnosesDTO diagnoses)
        {
            var data = new Diagnosis
            {
                DoctorId = diagnoses.DoctorId,
                UserId = diagnoses.UserId,
                Diagnosises = diagnoses.Diagnosises,
                Date = DateTime.Now,
            };
            _db.Diagnoses.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpGet("GetDiagnosesByUserId/{id}")]
        public IActionResult GetDiagnosesByUserId(int id)
        {
            var data = _db.Diagnoses
                .Where(x => x.UserId == id)
                .Select(diagnosis => new
                {
                    DiagnosisId = diagnosis.DiagnosisId,
                    Diagnosises = diagnosis.Diagnosises,
                    Date = diagnosis.Date,
                    DoctorName = diagnosis.Doctor.Name, 
                    UserName = $"{diagnosis.User.FirstName} {diagnosis.User.LastName}" 
                })
                .ToList();

            return Ok(data);
        }


    }
}
