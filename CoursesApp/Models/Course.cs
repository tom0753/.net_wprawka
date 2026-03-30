using System.ComponentModel.DataAnnotations;

namespace CoursesApp.Models;

public class Course
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytuł jest wymagany")]
    [MaxLength(200)]
    [Display(Name = "Tytuł")]
    public string Title { get; set; } = null!;

    [MaxLength(1000)]
    [Display(Name = "Opis")]
    public string? Description { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}