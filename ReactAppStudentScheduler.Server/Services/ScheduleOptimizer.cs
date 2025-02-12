using System;
using System.Collections.Generic;
using System.Linq;
using ReactAppStudentScheduler.Server.Models;
using StudentScheduler.Data;


namespace StudentScheduler.Services
{
    public class ScheduleOptimizer
    {
        private readonly AppDbContext _context;

        public ScheduleOptimizer(AppDbContext context)
        {
            _context = context;
        }

        public List<Schedule> GenerateOptimalSchedule(int studentId, List<int> requestedCourses)
        {
            List<Schedule> optimizedSchedule = new();
            var studentSchedules = _context.Schedules.Where(s => s.StudentId == studentId).ToList();

            foreach (var courseId in requestedCourses)
            {
                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
                if (course == null) continue; // הקורס לא קיים

                // בדיקה אם הקורס מלא
                int registeredStudents = _context.Schedules.Count(s => s.CourseId == course.Id);
                if (registeredStudents >= course.MaxCapacity)
                {
                    Console.WriteLine($"קורס {course.Name} מלא!");
                    continue;
                }

                // בדיקה אם יש התנגשות בלו"ז
                bool conflictExists = studentSchedules.Any(s =>
                    s.Day == course.Day &&
                    ((course.StartTime >= s.StartTime && course.StartTime < s.EndTime) ||
                     (course.EndTime > s.StartTime && course.EndTime <= s.EndTime)));

                if (!conflictExists)
                {
                    optimizedSchedule.Add(new Schedule
                    {
                        StudentId = studentId,
                        CourseId = course.Id,
                        Day = course.Day,
                        StartTime = course.StartTime,
                        EndTime = course.EndTime
                    });

                    // עדכון רשימת הלו"ז של הסטודנט
                    studentSchedules.Add(new Schedule
                    {
                        StudentId = studentId,
                        CourseId = course.Id,
                        Day = course.Day,
                        StartTime = course.StartTime,
                        EndTime = course.EndTime
                    });
                }
                else
                {
                    Console.WriteLine($"קורס {course.Name} מתנגש עם קורס אחר!");
                }
            }

            return optimizedSchedule;
        }
    }
}
