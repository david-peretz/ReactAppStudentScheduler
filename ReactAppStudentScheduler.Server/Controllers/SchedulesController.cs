using Microsoft.AspNetCore.Mvc;
using StudentScheduler.Data;

using StudentScheduler.Services;
using System.Collections.Generic;
using System.Linq;

namespace StudentScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ScheduleOptimizer _optimizer;

        public SchedulesController(AppDbContext context)
        {
            _context = context;
            _optimizer = new ScheduleOptimizer(context);
        }

        [HttpPost("optimize")]
        public IActionResult OptimizeSchedule([FromBody] ScheduleRequest request)
        {
            if (request == null || request.CourseIds == null || request.CourseIds.Count == 0)
                return BadRequest("Invalid schedule request.");

            var optimizedSchedule = _optimizer.GenerateOptimalSchedule(request.StudentId, request.CourseIds);

            if (optimizedSchedule.Count == 0)
                return Conflict("לא ניתן לשבץ אף קורס – כולם מלאים או שיש קונפליקטים!");

            _context.Schedules.AddRange(optimizedSchedule);
            _context.SaveChanges();

            return Ok(optimizedSchedule);
        }
    }

    public class ScheduleRequest
    {
        public int StudentId { get; set; }
        public List<int> CourseIds { get; set; }
    }
}
