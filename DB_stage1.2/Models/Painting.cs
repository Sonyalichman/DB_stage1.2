using System;
using System.Collections.Generic;

namespace DB_stage1._2.Models;

public partial class Painting
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Artist { get; set; }

    public DateTime? CreationStartDate { get; set; }

    public DateTime? CreationEndDate { get; set; }

    public int? TechiqueId { get; set; }

    public int? ExhibitionId { get; set; }

    public virtual Exhibition? Exhibition { get; set; }

    public virtual ICollection<Artist1> Artists { get; set; } = new List<Artist1>();

    public virtual ICollection<PaintingsTechnique> Techniques { get; set; } = new List<PaintingsTechnique>();
}
