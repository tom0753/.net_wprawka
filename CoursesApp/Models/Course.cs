namespace CoursesApp.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
