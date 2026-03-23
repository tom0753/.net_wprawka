namespace CoursesApp.Models;

public class Course
{
    public int Id { get; set; }                  // PK
    public string Title { get; set; } = null!;   // NOT NULL
    public string? Description { get; set; }     // NULL dopuszczalne

    // 1..N: jeden kurs ma wiele lekcji
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    // N..M: użytkownicy zapisani na kurs
    public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
