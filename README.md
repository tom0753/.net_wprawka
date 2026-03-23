# Baza danych Kursów Online

## Relacje
- **1..N**: Course → Lessons
- **N..M**: Users ↔ Courses (UserCourse)

## Migracje
dotnet ef migrations add InitialCreate
dotnet ef database update

## Schemat
- Users (Id PK, Email*, Name*)
- Courses (Id PK, Title*)
- Lessons (Id PK, CourseId FK*, Title*, Duration*)
- UserCourses (UserId+CourseId PK, EnrolledAt*)
