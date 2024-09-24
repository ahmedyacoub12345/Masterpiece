using MasterPieceBackEnd.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace MasterPeiceBackEnd.DTOs
{
    public class AddAppointmentDTO
    {
            public int DoctorId { get; set; }

            public DateTime Date { get; set; }

            public TimeOnly Time { get; set; }

            public bool Available { get; set; }
               
    }
}
