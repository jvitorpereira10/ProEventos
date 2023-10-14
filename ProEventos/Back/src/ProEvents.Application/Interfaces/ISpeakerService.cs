using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Application.Interfaces
{
    public interface ISpeakerService
    {
        Task<Speaker> AddSpeaker(Speaker model);
        Task<Speaker> UpdateSpeaker(int speakerId, Speaker model);
        Task<bool> DeleteSpeaker(int speakerId);

        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false);
        Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false);
        Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false);
    }
}