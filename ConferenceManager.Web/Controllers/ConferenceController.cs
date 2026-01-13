using Microsoft.AspNetCore.Mvc;
using ConferenceManager.Core.Entities;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ConferenceManager.Web.Controllers
{
    [Authorize]
    public class ConferenceController : Controller
    {
        // Імітуємо базу даних (поки що просто список у пам'яті)
        private static readonly List<Conference> _conferences = new List<Conference>
        {
            new Conference 
            { 
                Id = 1, 
                Name = ".NET Summit 2026", 
                Location = "Kyiv", 
                Date = DateTime.Now.AddDays(15),
                Description = "Largest .NET event"
            },
            new Conference 
            { 
                Id = 2, 
                Name = "AI & Future", 
                Location = "Lviv", 
                Date = DateTime.Now.AddDays(30),
                Description = "Artificial Intelligence conference"
            }
        };

        // GET: Conference
        public IActionResult Index()
        {
            return View(_conferences);
        }

        // GET: Conference/Details/5
        public IActionResult Details(int id)
        {
            var conf = _conferences.Find(c => c.Id == id);
            if (conf == null) return NotFound();
            return View(conf);
        }
    }
}