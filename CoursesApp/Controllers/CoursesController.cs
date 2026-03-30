using CoursesApp.Data;
using CoursesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursesApp.Controllers;

public class CoursesController : Controller
{
    private readonly AppDbContext _db;
    public CoursesController(AppDbContext db) => _db = db;

    // GET: / — lista z filtrowaniem
    public async Task<IActionResult> Index(string? search)
    {
        ViewBag.Search = search;
        var courses = _db.Courses
            .Include(c => c.Lessons)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            courses = courses.Where(c => c.Title.Contains(search));

        return View(await courses.ToListAsync());
    }

    // GET: /Courses/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var course = await _db.Courses
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return NotFound();
        return View(course);
    }

    // GET: /Courses/Create
    public IActionResult Create() => View();

    // POST: /Courses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        if (!ModelState.IsValid) return View(course);
        _db.Courses.Add(course);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: /Courses/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course == null) return NotFound();
        return View(course);
    }

    // POST: /Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.Id) return BadRequest();
        if (!ModelState.IsValid) return View(course);

        _db.Courses.Update(course);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: /Courses/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _db.Courses
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (course == null) return NotFound();
        return View(course);
    }

    // POST: /Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course != null) _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}