using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB_stage1._2.Models;

public partial class Genre
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Поле порожнє.")]
    [Display(Name = "Жанр")]
    public string Name { get; set; }

    public virtual ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();
}
