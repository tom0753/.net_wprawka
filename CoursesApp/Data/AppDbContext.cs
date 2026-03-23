using Microsoft.EntityFrameworkCore;
using CoursesApp.Models;

namespace CoursesApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Lesson> Lessons { get; set; } = null!;
    public DbSet<UserCourse> UserCourses { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) 
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Course
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.HasMany(e => e.Lessons).WithOne(e => e.Course).HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade);
        });

        // Lesson (1..N z Course)
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Duration).IsRequired();
        });

        // UserCourse (N..M)
        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CourseId });

            entity.HasOne(e => e.User)
                  .WithMany(u => u.UserCourses)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.UserCourses)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.EnrolledAt).IsRequired();
        });
    }
}
