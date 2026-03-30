using CoursesApp.Data;
using CoursesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursesApp.Controllers;

public class LessonsController : Controller
{
    private readonly AppDbContext _db;
    public LessonsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Create(int courseId)
    {
        var course = await _db.Courses.FindAsync(courseId);
        if (course == null) return NotFound();
        ViewBag.CourseName = course.Title;
        return View(new Lesson { CourseId = courseId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Lesson lesson)
    {
        if (!ModelState.IsValid)
        {
            var course = await _db.Courses.FindAsync(lesson.CourseId);
            ViewBag.CourseName = course?.Title;
            return View(lesson);
        }
        _db.Lessons.Add(lesson);
        await _db.SaveChangesAsync();
        return RedirectToAction("Details", "Courses", new { id = lesson.CourseId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var lesson = await _db.Lessons.FindAsync(id);
        if (lesson != null)
        {
            int courseId = lesson.CourseId;
            _db.Lessons.Remove(lesson);
            await _db.SaveChangesAsync();
            return RedirectToAction("Details", "Courses", new { id = courseId });
        }
        return RedirectToAction("Index", "Courses");
    }
}
