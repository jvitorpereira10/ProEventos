using System.Collections.Generic;

namespace ProEvents.Domain
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public string ImagemURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<SocialNetwork> SocialMedia { get; set; }
        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }
    }
}