using System;
using System.Collections.Generic;
using System.Numerics;

namespace MasterPieceBackEnd.Model;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int DoctorId { get; set; }

    public int Rating { get; set; }

    public string Reviews { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
