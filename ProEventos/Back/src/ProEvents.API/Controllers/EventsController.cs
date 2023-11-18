using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEvents.Persistence;
using ProEvents.Domain;
using ProEvents.Application.Interfaces;
using System;
using Microsoft.AspNetCore.Http;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var _events = await _eventService.GetAllEventsAsync(true);
                if (_events == null) return NotFound("No events found. ");

                return Ok(_events);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while trying to retrieve events. Error: {ex.Message}. ");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var _event = await _eventService.GetEventByIdAsync(id, true);
                if (_event == null) return NotFound("Event by id not found. ");

                return Ok(_event);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while trying to retrieve event by id. Error: {ex.Message}. ");
            }
        }

        [HttpGet("theme/{theme}")]
        public async Task<IActionResult> GetByTheme(string theme)
        {
            try
            {
                var _events = await _eventService.GetAllEventsByThemeAsync(theme, true);
                if (_events == null) return NotFound("Event by theme not found. ");

                return Ok(_events);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while trying to retrieve events by theme. Error: {ex.Message}. ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Event eventBody)
        {
            try
            {
                var _eventCreated = await _eventService.AddEvent(eventBody);
                if (_eventCreated == null) return BadRequest("Error while trying to create Event. ");

                return Created("", _eventCreated);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while trying to create Event. Error: {ex.Message}. ");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Event eventBody, int id)
        {
           try
            {
                var _eventUpdated = await _eventService.UpdateEvent(id, eventBody);
                if (_eventUpdated == null) return BadRequest("Error while trying to update Event. ");

                return Ok(_eventUpdated);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while trying to update Event. Error: {ex.Message}. ");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
           try
            {
                var _eventUpdated = await _eventService.DeleteEvent(id);
                if (!_eventUpdated) return BadRequest("Error while trying to delete Event. ");

                return Ok("Event deleted. ");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while trying to delete Event. Error: {ex.Message}. ");
            }
        }
    }
}