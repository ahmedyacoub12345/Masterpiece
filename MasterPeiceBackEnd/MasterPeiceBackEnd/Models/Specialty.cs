using System;
using System.Collections.Generic;
using System.Numerics;

namespace MasterPieceBackEnd.Model;

public partial class Specialty
{
    public int SpecialtyId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string? CategoryImage { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
