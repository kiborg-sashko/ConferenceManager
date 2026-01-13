using System;

namespace ConferenceManager.Core.Entities
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        // Метод для перевірки, чи конференція ще попереду (для тестів)
        public bool IsUpcoming()
        {
            return Date > DateTime.Now;
        }
    }
}