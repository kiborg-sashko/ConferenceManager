using Microsoft.EntityFrameworkCore;
using ConferenceManager.Core.Entities;

namespace ConferenceManager.Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Наші таблиці в БД
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}