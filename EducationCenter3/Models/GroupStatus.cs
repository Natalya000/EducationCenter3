using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class GroupStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
