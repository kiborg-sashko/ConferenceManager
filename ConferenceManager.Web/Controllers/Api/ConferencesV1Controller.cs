using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConferenceManager.Core.Data;
using ConferenceManager.Core.Entities;

namespace ConferenceManager.Web.Controllers.Api
{
    [ApiController]
    [ApiVersion("1.0")] // <--- Це перша версія
    [Route("api/v{version:apiVersion}/[controller]")] // Шлях буде /api/v1/conferences
    public class ConferencesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConferencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/conferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conference>>> GetConferences()
        {
            return await _context.Conferences.ToListAsync();
        }

        // GET: api/v1/conferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetConference(int id)
        {
            var conference = await _context.Conferences.FindAsync(id);
            if (conference == null) return NotFound();
            return conference;
        }
        [HttpPost]
public async Task<ActionResult<Conference>> PostConference(Conference conference)
{
    // Додаємо нову конференцію в базу даних
    _context.Conferences.Add(conference);
    await _context.SaveChangesAsync();

    // Повертаємо 201 Created та створений об'єкт
    return CreatedAtAction(nameof(GetConference), new { id = conference.Id, version = "1.0" }, conference);
}
    }
}