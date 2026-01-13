using System;
using ConferenceManager.Core.Entities;

namespace ConferenceManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Conference Management System v1.0 ===");

            // Створення конференції
            var conference = new Conference
            {
                Id = 1,
                Name = ".NET Conf 2026",
                Date = DateTime.Now.AddDays(20), // Конференція в майбутньому
                Location = "Kyiv, Online",
                Description = "The biggest .NET event in Ukraine"
            };

            // Перевірка логіки
            string status = conference.IsUpcoming() ? "Active (Upcoming)" : "Finished";

            Console.WriteLine($"\nConference: {conference.Name}");
            Console.WriteLine($"Location: {conference.Location}");
            Console.WriteLine($"Status: {status}");

            // Створення учасника
            var participant = new Participant
            {
                Id = 101,
                FullName = "Ivan Petrenko",
                Email = "ivan@example.com",
                Organization = "KNU"
            };

            Console.WriteLine($"\nParticipant Registered: {participant.FullName} ({participant.Organization})");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}