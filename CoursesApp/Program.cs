using CoursesApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=CoursesDb;Trusted_Connection=True;TrustServerCertificate=True"));

var serviceProvider = services.BuildServiceProvider();

// przykladowo
using var scope = serviceProvider.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();


