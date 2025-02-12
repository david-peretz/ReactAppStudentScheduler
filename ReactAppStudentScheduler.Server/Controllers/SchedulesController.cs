using Microsoft.AspNetCore.Mvc;
using ReactAppStudentScheduler.Server.Models;
using StudentScheduler.Data;

using System.Linq;

namespace StudentScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SchedulesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSchedules()
        {
            return Ok(_context.Schedules.ToList());
        }

        [HttpPost]
        public IActionResult CreateSchedule([FromBody] Schedule schedule)
        {
            if (schedule == null)
            {
                return BadRequest("Invalid schedule data");
            }

            _context.Schedules.Add(schedule);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSchedules), new { id = schedule.Id }, schedule);
        }
    }
}
