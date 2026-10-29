using System;
using System.Collections.Generic;

namespace EducationCenter3.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int PositionId { get; set; }

    public int DepartmentId { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public bool IsActive { get; set; }

    public string Note { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
