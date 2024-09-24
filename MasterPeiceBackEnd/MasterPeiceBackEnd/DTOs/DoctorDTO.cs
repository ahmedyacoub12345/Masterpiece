using MasterPieceBackEnd.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterPeiceBackEnd.DTOs
{
    public class DoctorDTO
    {
        public string Name { get; set; }
        public int SpecialtyId { get; set; }
        public int? UserId { get; set; }
        public string Qualifications { get; set; }
        public string ClinicAddress { get; set; }
        public string Phone { get; set; }
        public string? Availability { get; set; }
        public IFormFile? DoctorImage { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string? Degree { get; set; }
        public string? University { get; set; }


    }
}
