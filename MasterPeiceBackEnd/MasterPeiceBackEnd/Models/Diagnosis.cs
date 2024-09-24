using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class Diagnosis
{
    public int DiagnosisId { get; set; }

    public int DoctorId { get; set; }

    public int UserId { get; set; }

    public string Diagnosises { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
