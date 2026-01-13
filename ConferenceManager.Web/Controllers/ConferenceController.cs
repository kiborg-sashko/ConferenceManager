using Microsoft.AspNetCore.Mvc;
using ConferenceManager.Core.Entities;
using ConferenceManager.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceManager.Web.Controllers
{
    [Authorize]
    public class ConferenceController : Controller
    {
        private readonly AppDbContext _context;

        public ConferenceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Conference
       public async Task<IActionResult> Index(string searchString, DateTime? startDate, DateTime? endDate)
        {
            // 1. Створюємо заготовку запиту (ще не відправляємо в БД)
            var conferences = from c in _context.Conferences
                              select c;

            // 2. Якщо ввели текст — фільтруємо по Назві АБО Локації
            if (!string.IsNullOrEmpty(searchString))
            {
                conferences = conferences.Where(s => s.Name.Contains(searchString) 
                                       || s.Location.Contains(searchString));
            }

            // 3. Якщо вибрали початкову дату
            if (startDate.HasValue)
            {
                conferences = conferences.Where(c => c.Date >= startDate);
            }

            // 4. Якщо вибрали кінцеву дату
            if (endDate.HasValue)
            {
                conferences = conferences.Where(c => c.Date <= endDate);
            }

            // 5. Виконуємо запит і віддаємо результат
            return View(await conferences.ToListAsync());
        }

        // GET: Conference/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var conference = await _context.Conferences
                .FirstOrDefaultAsync(m => m.Id == id);

            if (conference == null) return NotFound();

            return View(conference);
        }
        // GET: Conference/Create (Показати форму)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conference/Create (Зберегти дані)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Conference conference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conference);
                await _context.SaveChangesAsync(); // <-- Тут магія запису в БД
                return RedirectToAction(nameof(Index));
            }
            return View(conference);
        }
        // GET: Conference/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var conference = await _context.Conferences.FindAsync(id);
            if (conference == null) return NotFound();

            return View(conference);
        }

        // POST: Conference/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Conference conference)
        {
            if (id != conference.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Conferences.Any(e => e.Id == conference.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(conference);
        }
        // GET: Conference/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var conference = await _context.Conferences
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (conference == null) return NotFound();

            return View(conference);
        }

        // POST: Conference/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conference = await _context.Conferences.FindAsync(id);
            if (conference != null)
            {
                _context.Conferences.Remove(conference);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}