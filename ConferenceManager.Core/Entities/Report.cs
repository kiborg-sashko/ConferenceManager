namespace ConferenceManager.Core.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; } // Короткий опис
        
        // Зв'язки
        public int ConferenceId { get; set; }
        public int ParticipantId { get; set; }
    }
}