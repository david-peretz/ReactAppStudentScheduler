using Microsoft.AspNetCore.Mvc;
using ReactAppStudentScheduler.Server.Models;
using StudentScheduler.Data;

using System.Linq;

namespace StudentScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok(_context.Courses.ToList());
        }

        [HttpPost]
        public IActionResult CreateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Invalid course data");
            }

            _context.Courses.Add(course);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCourses), new { id = course.Id }, course);
        }
    }
}
