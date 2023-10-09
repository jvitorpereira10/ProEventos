using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEvents.Persistence;
using ProEvents.Domain;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ProEventsContext _context;

        public EventsController(ProEventsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {           
            return _context.Events;
        }
        
        [HttpGet("{id}")]
        public async Task<Event> GetById(int id)
        {            
            return await _context.Events.FindAsync(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Event eventBody)
        {
            _context.Add(eventBody);
            await _context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<Event> Put([FromBody] Event eventBody, int id)
        {
            var eventToUpdate = await _context.Events.FindAsync(id);

            eventToUpdate.Local = !string.IsNullOrEmpty(eventBody.Local) ? eventBody.Local : eventToUpdate.Local;
            eventToUpdate.EventDate = eventBody.EventDate != null ? eventBody.EventDate : eventToUpdate.EventDate;
            eventToUpdate.Theme = !string.IsNullOrEmpty(eventBody.Theme) ? eventBody.Theme : eventToUpdate.Theme;
            eventToUpdate.AmountPeople = eventBody.AmountPeople != 0 ? eventBody.AmountPeople : eventToUpdate.AmountPeople;
            eventToUpdate.Batch = eventBody.Batch.Count() > 0 ? eventBody.Batch : eventToUpdate.Batch;
            eventToUpdate.ImageURL = !string.IsNullOrEmpty(eventBody.ImageURL) ? eventBody.ImageURL : eventToUpdate.ImageURL;

            _context.Entry(eventToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return eventToUpdate;
        }
        
        [HttpDelete]
        public async Task Delete(int? id)
        {
            var eventoToDelete = await _context.Events.FindAsync(id.Value);
            _context.Remove(eventoToDelete);
            await _context.SaveChangesAsync();
        }
    }
}