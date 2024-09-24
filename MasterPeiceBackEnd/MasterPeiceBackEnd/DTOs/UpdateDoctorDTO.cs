namespace MasterPeiceBackEnd.DTOs
{
    public class UpdateDoctorDTO
    {
        public int SpecialtyId { get; set; }

        public string Name { get; set; } = null!;

        public string Qualifications { get; set; } = null!;

        public string ClinicAddress { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string Availability { get; set; } = null!;
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public IFormFile? DoctorImage { get; set; }
        public string? Degree { get; set; }
        public string? University { get; set; }

    }
}
