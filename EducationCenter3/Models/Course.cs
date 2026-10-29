using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public int DurationHours { get; set; }

    public bool IsActive { get; set; }

    public string? Description { get; set; }

    public virtual CourseCategory Category { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
