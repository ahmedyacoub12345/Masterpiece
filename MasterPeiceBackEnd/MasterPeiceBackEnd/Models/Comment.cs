using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class Comment
{
    public int CommentId { get; set; }

    public int UserId { get; set; }

    public int DoctorId { get; set; }

    public string Comments { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
