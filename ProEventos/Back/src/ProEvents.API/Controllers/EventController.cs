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
    public class EventController : ControllerBase
    {
        private readonly DataContext _context;

        public EventController(DataContext context)
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
            var eventoToUpdate = await _context.Events.FindAsync(id);

            eventoToUpdate.Local = !string.IsNullOrEmpty(eventBody.Local) ? eventBody.Local : eventoToUpdate.Local;
            eventoToUpdate.EventDate = !string.IsNullOrEmpty(eventBody.EventDate) ? eventBody.EventDate : eventoToUpdate.EventDate;
            eventoToUpdate.Theme = !string.IsNullOrEmpty(eventBody.Theme) ? eventBody.Theme : eventoToUpdate.Theme;
            eventoToUpdate.AmountPeople = !string.IsNullOrEmpty(eventBody.AmountPeople.ToString()) ? eventBody.AmountPeople : eventoToUpdate.AmountPeople;
            eventoToUpdate.Batch = !string.IsNullOrEmpty(eventBody.Batch) ? eventBody.Batch : eventoToUpdate.Batch;
            eventoToUpdate.ImageURL = !string.IsNullOrEmpty(eventBody.ImageURL) ? eventBody.ImageURL : eventoToUpdate.ImageURL;

            _context.Entry(eventoToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return eventoToUpdate;
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