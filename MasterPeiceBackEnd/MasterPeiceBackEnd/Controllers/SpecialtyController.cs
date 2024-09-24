using MasterPeiceBackEnd.DTOs;
using MasterPeiceBackEnd.Shared;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MasterPeiceBackEnd.Shared.ImageSaver;
namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        public SpecialtyController(MedicalAppContext db)
        {
            _db = db;
        }
        [HttpGet("GetAllSepecialties")]
        public IActionResult GetAllSpecialties()
        {
            var data = _db.Specialties.ToList();
            return Ok(data);
        }
        [HttpGet("GetSpecialtyById/{id}")]
        public IActionResult Get(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var data = _db.Specialties.Find(id);

            return Ok(data);
        }
        [HttpDelete("DeleteSpecialtyById/{id}")]
        public IActionResult Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var data = _db.Specialties.FirstOrDefault(p => p.SpecialtyId == id);
                _db.Specialties.Remove(data);
                _db.SaveChanges();
                return Ok(data);
        }
        [HttpPost("CreateNewSpecialties")]
        public IActionResult CreateSpeciaties([FromForm] SpecialtyRequestDTO response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var data = new Specialty
            {
                Name = response.Name,
                Description = response.Description,
                CategoryImage = SaveImage(response.CategoryImage),
            };
            var request = _db.Specialties.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpPut("UpdateSpecialtyBuId/{id:int}")]
        public IActionResult UpdateCategory([FromForm] SpecialtyRequestDTO response, int id)
        {

            var category = _db.Specialties.Find(id);
            if (category == null) return NotFound();
            category.CategoryImage = SaveImage(response.CategoryImage);
            category.Name = response.Name;
            category.Description = response.Description;

            var request = _db.Specialties.Update(category);
            _db.SaveChanges();
            return Ok(category);
        }
    }
}
