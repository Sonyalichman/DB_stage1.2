using System;
using System.Collections.Generic;

namespace DB_stage1._2.Models;

public partial class Exhibition
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? GenreId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Painting> Paintings { get; set; } = new List<Painting>();

    public virtual ICollection<Guide> Guides { get; set; } = new List<Guide>();
}
