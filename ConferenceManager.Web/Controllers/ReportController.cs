using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Для випадаючих списків
using Microsoft.EntityFrameworkCore;
using ConferenceManager.Core.Data;
using ConferenceManager.Core.Entities;

namespace ConferenceManager.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Report (Тут буде наш пошук)
        public async Task<IActionResult> Index(string searchString, int? conferenceId, DateTime? startDate, DateTime? endDate)
        {
            // 1. Починаємо запит. 
            // Include() - це і є наші JOIN операції!
            // Ми "підтягуємо" дані з таблиць Conference та Participant.
            var reports = _context.Reports
                .Include(r => r.Conference)  // JOIN 1
                .Include(r => r.Participant) // JOIN 2
                .AsQueryable();

            // 2. Фільтр по тексту (початок або кінець або середина)
            if (!string.IsNullOrEmpty(searchString))
            {
                reports = reports.Where(r => r.Title.Contains(searchString));
            }

            // 3. Фільтр по списку (вибір конкретної конференції)
            if (conferenceId.HasValue)
            {
                reports = reports.Where(r => r.ConferenceId == conferenceId);
            }

            // 4. Фільтр по Даті (Date range)
            if (startDate.HasValue)
            {
                reports = reports.Where(r => r.Conference.Date >= startDate);
            }
            if (endDate.HasValue)
            {
                reports = reports.Where(r => r.Conference.Date <= endDate);
            }

            // Заповнюємо випадаючий список для фільтру
            ViewData["ConferenceId"] = new SelectList(_context.Conferences, "Id", "Name");
            
            return View(await reports.ToListAsync());
        }
    }
}