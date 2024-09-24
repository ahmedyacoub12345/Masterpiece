using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int DoctorId { get; set; }

    public DateTime Date { get; set; }

    public TimeOnly Time { get; set; }

    public bool Available { get; set; }

    //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    //public virtual Doctor Doctor { get; set; } = null!;
}