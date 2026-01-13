using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConferenceManager.Core.Entities;
using ConferenceManager.Core.Data;

namespace ConferenceManager.Web.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly AppDbContext _context;

        public ParticipantController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Participant (Список)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Participants.ToListAsync());
        }

        // GET: Participant/Create (Форма створення)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participant/Create (Збереження)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participant participant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        // GET: Participant/Edit/5 (Форма редагування)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null) return NotFound();
            return View(participant);
        }

        // POST: Participant/Edit/5 (Збереження змін)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Participant participant)
        {
            if (id != participant.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        // GET: Participant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var participant = await _context.Participants.FirstOrDefaultAsync(m => m.Id == id);
            if (participant == null) return NotFound();
            return View(participant);
        }

        // POST: Participant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant != null) {
                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}