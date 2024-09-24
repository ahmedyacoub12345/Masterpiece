using System;
using System.Collections.Generic;
using System.Numerics;

namespace MasterPieceBackEnd.Model;

public partial class Treatment
{
    public int TreatmentId { get; set; }

    public int DoctorId { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
