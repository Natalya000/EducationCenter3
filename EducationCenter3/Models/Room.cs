using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class Room
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public string Building { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();
}
