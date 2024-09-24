namespace MasterPeiceBackEnd.DTOs
{
    public class RequestBookingDTO
    {
        public int UserId { get; set; }

        public int DoctorId { get; set; }

        public TimeOnly Time { get; set; }

        public DateTime BookingDate { get; set; }

        public string PaymentStatus { get; set; } = null!;
    }
}
