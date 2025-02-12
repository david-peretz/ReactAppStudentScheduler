
using global::StudentScheduler.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReactAppStudentScheduler.Server.Models;


using System;
using System.Linq;
namespace ReactAppStudentScheduler.Server.SeedData
{

    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Courses.Any() || context.Students.Any() || context.Schedules.Any())
            {
                return; // Database has been seeded
            }

            var courses = new Course[]
            {
                    new() { Name = "Mathematics 101", MaxCapacity = 30, Day = "Monday", StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") },
                    new() { Name = "Physics 202", MaxCapacity = 25, Day = "Tuesday", StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:00") },
                    new() { Name = "Chemistry 303", MaxCapacity = 20, Day = "Wednesday", StartTime = TimeSpan.Parse("08:00"), EndTime = TimeSpan.Parse("10:00") }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            var students = new Student[]
            {
                    new() { Name = "Alice Johnson", SelectedCourses = new() { courses[0].Id, courses[1].Id } },
                    new() { Name = "Bob Smith", SelectedCourses = new() { courses[1].Id, courses[2].Id } }
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            var schedules = new Schedule[]
            {
                    new() { StudentId = students[0].Id, CourseId = courses[0].Id, Day = "Monday", StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") },
                    new() { StudentId = students[1].Id, CourseId = courses[2].Id, Day = "Wednesday", StartTime = TimeSpan.Parse("08:00"), EndTime = TimeSpan.Parse("10:00") }
            };
            context.Schedules.AddRange(schedules);
            context.SaveChanges();
        }
    }
    }


