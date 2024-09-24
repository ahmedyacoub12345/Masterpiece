using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class AdminAction
{
    public int AdminActionId { get; set; }

    public int AdminId { get; set; }

    public string ActionType { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Details { get; set; } = null!;

    public virtual Admin Admin { get; set; } = null!;
}
