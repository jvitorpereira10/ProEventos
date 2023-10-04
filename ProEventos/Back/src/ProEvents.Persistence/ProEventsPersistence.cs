using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;

namespace ProEvents.Persistence
{
    public class ProEventsPersistence : IProEventsPersistence
    {
        private readonly ProEventsContext _context;
        public ProEventsPersistence(ProEventsContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                    .Include(e => e.Batch)
                    .Include(e => e.SocialMedia);

            if (includeSpeakers)
            {
                query = query
                    .Include(e => e.EventSpeakers)
                    .ThenInclude(se => se.Speaker);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                    .Include(e => e.Batch)
                    .Include(e => e.SocialMedia);

            if (includeSpeakers)
            {
                query = query
                    .Include(e => e.EventSpeakers)
                    .ThenInclude(se => se.Speaker);
            }

            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                     .Include(e => e.Batch)
                     .Include(e => e.SocialMedia);

            if (includeSpeakers)
            {
                query = query
                    .Include(e => e.EventSpeakers)
                    .ThenInclude(se => se.Speaker);
            }

            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }

        public Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false)
        {
            throw new NotImplementedException();
        }
    }
}