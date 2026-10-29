using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EducationCenter3.Models;

public partial class EducationCenterContext : DbContext
{
    public EducationCenterContext()
    {
    }

    public EducationCenterContext(DbContextOptions<EducationCenterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseCategory> CourseCategories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<EnrollmentStatus> EnrollmentStatuses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupStatus> GroupStatuses { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<ScheduleItem> ScheduleItems { get; set; }

    public virtual DbSet<ScheduleType> ScheduleTypes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentStatus> StudentStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=DESKTOP-MJQF25J\\SQLEXPRESS;Database=EducationCenter;TrustServerCertificate=true;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => new { e.CategoryId, e.Name }, "UQ_Courses_Category_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_CourseCategories");
        });

        modelBuilder.Entity<CourseCategory>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_CourseCategories_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Departments_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.FullName, "IX_Employees_FullName");

            entity.HasIndex(e => e.Email, "UX_Employees_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.HireDate).HasDefaultValueSql("(CONVERT([date],sysdatetime()))");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(30);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Departments");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Positions");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasIndex(e => e.StudentId, "IX_Enrollments_StudentId");

            entity.HasIndex(e => new { e.GroupId, e.StudentId }, "UQ_Enrollments_Group_Student").IsUnique();

            entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Group).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Groups");

            entity.HasOne(d => d.Status).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_EnrollmentStatuses");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Students");
        });

        modelBuilder.Entity<EnrollmentStatus>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_EnrollmentStatuses_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasIndex(e => e.CourseId, "IX_Groups_CourseId");

            entity.HasIndex(e => e.Name, "UQ_Groups_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_Courses");

            entity.HasOne(d => d.ResponsibleEmployee).WithMany(p => p.Groups)
                .HasForeignKey(d => d.ResponsibleEmployeeId)
                .HasConstraintName("FK_Groups_Employees");

            entity.HasOne(d => d.Status).WithMany(p => p.Groups)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_GroupStatuses");
        });

        modelBuilder.Entity<GroupStatus>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_GroupStatuses_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Positions_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasIndex(e => new { e.Building, e.Number }, "UQ_Rooms_Building_Number").IsUnique();

            entity.Property(e => e.Building).HasMaxLength(100);
            entity.Property(e => e.Number).HasMaxLength(20);
        });

        modelBuilder.Entity<ScheduleItem>(entity =>
        {
            entity.HasIndex(e => e.LessonDate, "IX_ScheduleItems_LessonDate");

            entity.Property(e => e.Note).HasMaxLength(255);

            entity.HasOne(d => d.Group).WithMany(p => p.ScheduleItems)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleItems_Groups");

            entity.HasOne(d => d.InstructorEmployee).WithMany(p => p.ScheduleItems)
                .HasForeignKey(d => d.InstructorEmployeeId)
                .HasConstraintName("FK_ScheduleItems_Employees");

            entity.HasOne(d => d.Room).WithMany(p => p.ScheduleItems)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleItems_Rooms");

            entity.HasOne(d => d.ScheduleType).WithMany(p => p.ScheduleItems)
                .HasForeignKey(d => d.ScheduleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleItems_ScheduleTypes");
        });

        modelBuilder.Entity<ScheduleType>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_ScheduleTypes_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.FullName, "IX_Students_FullName");

            entity.HasIndex(e => e.Email, "UX_Students_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(30);

            entity.HasOne(d => d.Status).WithMany(p => p.Students)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_StudentStatuses");
        });

        modelBuilder.Entity<StudentStatus>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_StudentStatuses_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Login).HasMaxLength(80);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Employees");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
