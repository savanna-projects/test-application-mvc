using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        private readonly SchoolContext _context;

        public MockController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Mock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return await _context.Students.ToListAsync().ConfigureAwait(false);
        }

        // POST: api/Mock
        [HttpPost]
        public IActionResult Post()
        {
            _ = _context;
            return NoContent();
        }
    }
}