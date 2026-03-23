namespace CoursesApp.Models;

public class User
{
    public int Id { get; set; }              // PK
    public string Email { get; set; } = null!;   // NOT NULL
    public string Name { get; set; } = null!;    // NOT NULL

    // N..M: wiele kursów zapisanych przez użytkownika
    public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
