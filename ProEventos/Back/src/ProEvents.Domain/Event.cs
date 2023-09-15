using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvents.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? EventDate { get; set; }
        public string Theme { get; set; }
        public int AmountPeople { get; set; }        
        public string ImageURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Batch> Batch { get; set; }
        public IEnumerable<SocialNetwork> SocialMedia { get; set; }
        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }

    }
}