using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConferenceManager.Core.Data;
using ConferenceManager.Core.Entities;

namespace ConferenceManager.Web.Controllers.Api
{
    [ApiController]
    [ApiVersion("2.0")] // <--- Це ДРУГА версія
    [Route("api/v{version:apiVersion}/conferences")] // Шлях буде /api/v2/conferences
    public class ConferencesV2Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConferencesV2Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v2/conferences
        // У версії 2.0 ми, наприклад, сортуємо їх за датою (це і є "зміна")
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conference>>> GetConferences()
        {
            return await _context.Conferences
                                 .OrderByDescending(c => c.Date) // Сортування - відмінність v2
                                 .ToListAsync();
        }
    }
}