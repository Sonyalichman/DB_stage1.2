using System;
using System.Collections.Generic;

namespace DB_stage1._2.Models;

public partial class Artist1
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Nickname { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfDeath { get; set; }

    public virtual ICollection<Painting> Paintings { get; set; } = new List<Painting>();
}
