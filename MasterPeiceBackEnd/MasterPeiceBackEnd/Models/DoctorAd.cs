using System;
using System.Collections.Generic;
using System.Numerics;

namespace MasterPieceBackEnd.Model;

public partial class DoctorAd
{
    public int AdId { get; set; }

    public int DoctorId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime PublishedAt { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}
