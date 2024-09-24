using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class Admin
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public string Department { get; set; } = null!;

    public virtual ICollection<AdminAction> AdminActions { get; set; } = new List<AdminAction>();

    public virtual User User { get; set; } = null!;
}
