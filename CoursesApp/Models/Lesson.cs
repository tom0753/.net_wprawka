namespace CoursesApp.Models;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public TimeSpan Duration { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}
