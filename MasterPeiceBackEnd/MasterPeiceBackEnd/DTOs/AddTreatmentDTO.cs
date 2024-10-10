namespace MasterPeiceBackEnd.DTOs
{
    public class AddTreatmentDTO
    {
        public int DoctorId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; } = null!;
    }
}
