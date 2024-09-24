using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int DoctorId { get; set; }

    public TimeOnly Time { get; set; }

    public DateTime BookingDate { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
    public virtual Doctor Doctor { get; set; } = null!;

}
