using CoursesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesApp.Data;

public class AppDbContext : DbContext
{
    // Tabele
    public DbSet<User> Users => Set<User>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<UserCourse> UserCourses => Set<UserCourse>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(200);
            entity.Property(u => u.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // Course
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Title)
                  .IsRequired()
                  .HasMaxLength(200);
            entity.Property(c => c.Description)
                  .HasMaxLength(1000);
        });

        // Lesson – relacja 1..N: Course → Lesson
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Title)
                  .IsRequired()
                  .HasMaxLength(200);
            entity.Property(l => l.Duration)
                  .IsRequired();

            entity.HasOne(l => l.Course)        // Lesson ma jeden Course
                  .WithMany(c => c.Lessons)     // Course ma wiele Lessons
                  .HasForeignKey(l => l.CourseId)
                  .OnDelete(DeleteBehavior.Cascade); // FK NOT NULL, usuwanie kaskadowe [web:1][web:4]
        });

        // UserCourse – relacja N..M z tabelą pośrednią
        modelBuilder.Entity<UserCourse>(entity =>
        {
            // złożony klucz główny
            entity.HasKey(uc => new { uc.UserId, uc.CourseId });

            entity.Property(uc => uc.EnrolledAt)
                  .IsRequired();

            entity.HasOne(uc => uc.User)
                  .WithMany(u => u.UserCourses)
                  .HasForeignKey(uc => uc.UserId);

            entity.HasOne(uc => uc.Course)
                  .WithMany(c => c.UserCourses)
                  .HasForeignKey(uc => uc.CourseId);
        });
    }
}
