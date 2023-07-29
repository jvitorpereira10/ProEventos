using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEvents.API.Data;
using ProEvents.API.Models;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {           
            return _context.Events;
        }
        
        [HttpGet("{id}")]
        public async Task<Event> Get(int id)
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
            eventToUpdate.EventDate = !string.IsNullOrEmpty(eventBody.EventDate) ? eventBody.EventDate : eventToUpdate.EventDate;
            eventToUpdate.Theme = !string.IsNullOrEmpty(eventBody.Theme) ? eventBody.Theme : eventToUpdate.Theme;
            eventToUpdate.AmountPeople = eventBody.AmountPeople != 0 ? eventBody.AmountPeople : eventToUpdate.AmountPeople;
            eventToUpdate.Batch = !string.IsNullOrEmpty(eventBody.Batch) ? eventBody.Batch : eventToUpdate.Batch;
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