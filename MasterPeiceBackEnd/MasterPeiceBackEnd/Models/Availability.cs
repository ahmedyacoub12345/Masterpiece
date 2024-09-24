using MasterPieceBackEnd.Model;

namespace MasterPeiceBackEnd.Models
{
    public class Availability
    {
        public int AvailabilityId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
    }
}
