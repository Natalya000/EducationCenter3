using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class CourseCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
