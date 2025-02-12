using Microsoft.AspNetCore.Mvc;
using ReactAppStudentScheduler.Server.Models;
using StudentScheduler.Data;

using System.Linq;

namespace StudentScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_context.Students.ToList());
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data");
            }

            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
        }
    }
}
