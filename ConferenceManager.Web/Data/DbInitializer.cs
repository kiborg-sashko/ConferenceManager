using ConferenceManager.Core.Entities;
using ConferenceManager.Core.Data;
using System;
using System.Linq;

namespace ConferenceManager.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Якщо є хоча б одна конференція — виходимо, база вже заповнена
            if (context.Conferences.Any())
            {
                return;
            }

            // 1. Створюємо конференції
            var conferences = new Conference[]
            {
                new Conference { Name = "DotNet Fest 2026", Location = "Kyiv", Date = DateTime.Parse("2026-05-20"), Description = "All about .NET" },
                new Conference { Name = "AI Future", Location = "Lviv", Date = DateTime.Parse("2026-09-15"), Description = "Machine Learning trends" }
            };
            context.Conferences.AddRange(conferences);
            context.SaveChanges();

            // 2. Створюємо учасників
            var participants = new Participant[]
            {
                new Participant { FullName = "Ivan Petrenko", Email = "ivan@test.com", Organization = "NURE" },
                new Participant { FullName = "Maria Koval", Email = "maria@test.com", Organization = "SoftServe" }
            };
            context.Participants.AddRange(participants);
            context.SaveChanges();

            // 3. Створюємо доповіді (Зв'язуємо Конференції та Учасників)
            var reports = new Report[]
            {
                new Report { Title = "Entity Framework Core Basic", Abstract = "Intro to EF", ConferenceId = conferences[0].Id, ParticipantId = participants[0].Id },
                new Report { Title = "AI in Healthcare", Abstract = "Medical AI", ConferenceId = conferences[1].Id, ParticipantId = participants[1].Id }
            };
            context.Reports.AddRange(reports);
            context.SaveChanges();
        }
    }
}