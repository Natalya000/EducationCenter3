using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class EnrollmentStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
