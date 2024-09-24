namespace MasterPeiceBackEnd.DTOs
{
    public class UpdateAvailabilityDTO
    {
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
