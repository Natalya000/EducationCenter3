using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class ScheduleItem
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int RoomId { get; set; }

    public int? InstructorEmployeeId { get; set; }

    public DateOnly LessonDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int ScheduleTypeId { get; set; }

    public string? Note { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Employee? InstructorEmployee { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual ScheduleType ScheduleType { get; set; } = null!;
}
