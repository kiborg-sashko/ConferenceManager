using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Core.Entities
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Abstract { get; set; }

        public int ConferenceId { get; set; }
        public int ParticipantId { get; set; }

        public virtual Conference Conference { get; set; }
        public virtual Participant Participant { get; set; }
    }
}