namespace MasterPeiceBackEnd.DTOs
{
    public class UpdateAppointmentDTO
    {
        public DateTime Date { get; set; }

        public TimeOnly Time { get; set; }

        public bool Available { get; set; }
    }
}
