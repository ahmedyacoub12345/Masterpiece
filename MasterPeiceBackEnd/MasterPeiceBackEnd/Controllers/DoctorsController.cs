﻿using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MasterPeiceBackEnd.Shared.ImageSaver;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        public DoctorsController(MedicalAppContext db)
        {
            _db = db;
        }
        [HttpGet("GetAllDoctors")]
        public IActionResult GetAllDoctors()
        {
            var data = _db.Doctors.ToList();
            return Ok(data);
        }
        [HttpGet("GetDoctorById/{id:int}")]
        public IActionResult GetDoctors(int id) 
        {
            var data = _db.Doctors.Find(id);
            return Ok(data);
        }
        [HttpPost("AddNewDoctor")]
        public IActionResult AddDoctor([FromForm] DoctorDTO doctor)
        {
            var data = new Doctor
            {
                Name = doctor.Name,
                ClinicAddress = doctor.ClinicAddress,
                SpecialtyId = doctor.SpecialtyId,
                Phone = doctor.Phone,
                Qualifications = doctor.Qualifications,
                Availability = doctor.Availability,
                UserId = doctor.UserId,
                DoctorImage = SaveImage(doctor.DoctorImage),
                Email= doctor.Email,
                Description= doctor.Description,
                Degree = doctor.Degree,
                University = doctor.University,
            };
            var request = _db.Doctors.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpPut("UpdateDoctor/{id:int}")]
        public IActionResult UpdateDoctor(int id , [FromForm] UpdateDoctorDTO doctor)
        {
            var data = _db.Doctors.Find(id);

            if (doctor.Name != null)
                data.Name = doctor.Name;
            if (doctor?.SpecialtyId != null)
                data.SpecialtyId = doctor.SpecialtyId;
            if (doctor?.Phone != null)
                data.Phone = doctor.Phone;
            if (doctor?.UserId != null)
                data.UserId = doctor.UserId;
            if (doctor?.Email != null)
                data.Email = doctor.Email;
            if (doctor?.Description != null)
                data.Description = doctor.Description;
            if (doctor?.DoctorImage != null)
                data.DoctorImage = SaveImage( doctor.DoctorImage);
            if (doctor?.Degree != null)
                data.Degree = doctor.Degree;
            if (doctor?.University != null)
                data.University = doctor.University;

            var request = _db.Doctors.Update(data);
            _db.SaveChanges();
            return Ok(data);

        }
        [HttpGet("/Api/Products/GetDoctorsBySpecialtyId/{id}")]
        public IActionResult GetAction(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var specialty = _db.Specialties.Find(id);
            if (specialty == null)
            {
                return NotFound();
            }

            var doctors = _db.Doctors
                .Include(d => d.Specialty) // Eager load the Specialty
                .Where(d => d.SpecialtyId == specialty.SpecialtyId) // Filter by SpecialtyId
                .Select(d => new
                {
                    d.DoctorId,
                    d.Name,
                    d.Description,
                    d.Email,
                    d.Qualifications,
                    d.ClinicAddress,
                    d.Phone,
                    d.Availability,
                    d.DoctorImage,
                    SpecialtyName = d.Specialty.Name // Select the specialty name
                })
                .ToList(); // Execute the query

            if (doctors.Any())
            {
                return Ok(doctors);
            }

            return NotFound();
        }
        [HttpGet("/Api/Products/GetDoctorDetails/{id}")]
        public IActionResult GetDoctorDetails(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var specialty = _db.Doctors.Find(id);
            if (specialty == null)
            {
                return NotFound();
            }

            var doctors = _db.Doctors
                .Include(d => d.Specialty) 
                .Where(d => d.DoctorId == specialty.DoctorId) 
                .Select(d => new
                {
                    d.DoctorId,
                    d.Name,
                    d.Description,
                    d.Email,
                    d.Qualifications,
                    d.ClinicAddress,
                    d.Phone,
                    d.Availability,
                    d.DoctorImage,
                    d.University,
                    d.Degree,
                    SpecialtyName = d.Specialty.Name // Select the specialty name
                })
                .ToList(); // Execute the query

            if (doctors.Any())
            {
                return Ok(doctors);
            }

            return NotFound();
        }


    }
}