using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Persistence
{
    public class SpeakerPersistence : ISpeakerPersistence
    {
        private readonly ProEventsContext _context;
        public SpeakerPersistence(ProEventsContext context)
        {
            _context = context;
        }

        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents)
        {
            IQueryable<Speaker> query = GetSpeakersBase(includeEvents).OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false)
        {
            IQueryable<Speaker> query = GetSpeakersBase(includeEvents);

            query = query.Where(s => s.Name.ToLower().Contains(name))
                         .OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false)
        {
            IQueryable<Speaker> query = GetSpeakersBase(includeEvents);

            return await query.FirstOrDefaultAsync(s => s.Id == speakerId);
        }

        private IQueryable<Speaker> GetSpeakersBase(bool includeEvents)
        {
            IQueryable<Speaker> query = _context.Speakers
                    .Include(s => s.SocialMedia);

            if (includeEvents)
            {
                query = query
                    .Include(s => s.EventSpeakers)
                    .ThenInclude(se => se.Event);
            }

            return query;
        }
    }
}