namespace CoursesApp.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
