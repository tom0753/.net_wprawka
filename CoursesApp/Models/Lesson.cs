namespace CoursesApp.Models;

public class Lesson
{
    public int Id { get; set; }                  // PK
    public string Title { get; set; } = null!;   // NOT NULL
    public TimeSpan Duration { get; set; }       // NOT NULL

    // FK do Course (1..N)
    public int CourseId { get; set; }            // NOT NULL
    public Course Course { get; set; } = null!;  // nawigacja
}
