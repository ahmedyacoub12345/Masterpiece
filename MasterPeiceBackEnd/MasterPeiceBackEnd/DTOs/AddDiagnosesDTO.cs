namespace MasterPeiceBackEnd.DTOs
{
    public class AddDiagnosesDTO
    {
        public int DoctorId { get; set; }

        public int UserId { get; set; }

        public string Diagnosises { get; set; } = null!;

    }
}
