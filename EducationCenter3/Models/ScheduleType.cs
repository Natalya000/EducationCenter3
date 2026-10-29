using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class ScheduleType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();
}
