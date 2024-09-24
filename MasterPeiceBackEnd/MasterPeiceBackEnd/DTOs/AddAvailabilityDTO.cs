namespace MasterPeiceBackEnd.DTOs
{
    public class AddAvailabilityDTO
    {
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
