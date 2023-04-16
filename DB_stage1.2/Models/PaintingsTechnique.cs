using System;
using System.Collections.Generic;

namespace DB_stage1._2.Models;

public partial class PaintingsTechnique
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Painting> Paintings { get; set; } = new List<Painting>();
}
