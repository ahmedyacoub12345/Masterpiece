namespace MasterPeiceBackEnd.DTOs
{
    public class RequestBookingDTO
    {
        public int UserId { get; set; }

        public int DoctorId { get; set; }

        public TimeOnly Time { get; set; }

    }
}
