using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CoursesApp.Models;

public class Lesson
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytuł lekcji jest wymagany")]
    [MaxLength(200)]
    [Display(Name = "Tytuł lekcji")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Czas trwania jest wymagany")]
    [Display(Name = "Czas trwania (minuty)")]
    [Range(1, 600, ErrorMessage = "Czas musi być między 1 a 600 minut")]
    public int DurationMinutes { get; set; }

    public int CourseId { get; set; }

    [ValidateNever]
    public Course Course { get; set; } = null!;
}
