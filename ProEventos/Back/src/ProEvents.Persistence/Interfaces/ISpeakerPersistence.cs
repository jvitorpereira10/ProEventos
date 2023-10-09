using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Persistence.Interfaces
{
    public interface ISpeakerPersistence
    {
        // SPEAKER
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false);
        Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false);
        Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false);
    }
}