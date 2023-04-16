using System;
using System.Collections.Generic;

namespace DB_stage1._2.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();
}
