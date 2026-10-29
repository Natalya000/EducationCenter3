using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class Enrollment
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int StudentId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public int StatusId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual EnrollmentStatus Status { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
