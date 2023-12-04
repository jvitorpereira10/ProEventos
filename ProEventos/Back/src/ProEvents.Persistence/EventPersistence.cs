using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Persistence
{
    public class EventPersistence : IEventPersistence
    {
        private readonly ProEventsContext _context;
        public EventPersistence(ProEventsContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            IQueryable<Event> query = GetEventsBase(includeSpeakers);

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false)
        {
            IQueryable<Event> query = GetEventsBase(includeSpeakers);

            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int? eventId, bool includeSpeakers = false)
        {
            if (eventId == null) return null;
            IQueryable<Event> query = GetEventsBase(includeSpeakers);

            return await query.FirstOrDefaultAsync(e => e.Id == eventId);
        }

        private IQueryable<Event> GetEventsBase(bool includeSpeakers)
        {
            IQueryable<Event> query = _context.Events
                    .Include(e => e.Batches)
                    .Include(e => e.SocialMedia);

            if (includeSpeakers)
            {
                query = query
                    .Include(e => e.EventSpeakers)
                    .ThenInclude(se => se.Speaker);
            }

            return query;
        }
    }
}