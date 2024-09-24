using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime PublishedAt { get; set; }
}
