using System;
using System.Linq;
using System.Threading.Tasks;
using ProEvents.Application.Interfaces;
using ProEvents.Domain;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Application
{
    public class EventService : IEventService
    {
        public IGenericPersistence _genericPersistence { get; }
        public IEventPersistence _eventPersistence { get; set; }

        public EventService(IGenericPersistence genericPersistence, IEventPersistence eventPersistence)
        {
            _eventPersistence = eventPersistence;
            _genericPersistence = genericPersistence;
        }

        public async Task<Event> AddEvent(Event model)
        {
            try
            {
                _genericPersistence.Add<Event>(model); // Verificar necessidade de usar a classe explícita no método
                if (await _genericPersistence.SaveChangesAsync())
                {
                    return await _eventPersistence.GetEventByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Event> UpdateEvent(int eventId, Event model)
        {
            try
            {
                var _eventToUpdate = await _eventPersistence.GetEventByIdAsync(eventId);
                if (_eventToUpdate == null) return null;

                // _eventToUpdate.Local = !string.IsNullOrWhiteSpace(model.Local) ? model.Local : _eventToUpdate.Local;
                // _eventToUpdate.EventDate = model.EventDate != null ? model.EventDate : _eventToUpdate.EventDate;
                // _eventToUpdate.Theme = !string.IsNullOrWhiteSpace(model.Theme) ? model.Theme : _eventToUpdate.Theme;
                // _eventToUpdate.AmountPeople = model.AmountPeople != 0 ? model.AmountPeople : _eventToUpdate.AmountPeople;
                // _eventToUpdate.Batch = model.Batch.Count() > 0 ? model.Batch : _eventToUpdate.Batch;
                // _eventToUpdate.ImageURL = !string.IsNullOrWhiteSpace(model.ImageURL) ? model.ImageURL : _eventToUpdate.ImageURL;

                model.Id = eventId;

                _genericPersistence.Update<Event>(model);
                if (await _genericPersistence.SaveChangesAsync())
                {
                    return await _eventPersistence.GetEventByIdAsync(eventId);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> DeleteEvent(int? eventId)
        {
            try
            {
                var _eventToDelete = await _eventPersistence.GetEventByIdAsync(eventId);
                if (_eventToDelete == null) throw new Exception($"Event to delete not found. ");

                _genericPersistence.Delete<Event>(_eventToDelete);
                return await _genericPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            try
            {
                var _events = await _eventPersistence.GetAllEventsAsync(includeSpeakers);
                if (_events == null) return null;

                return _events;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false)
        {
            try
            {
                var _events = await _eventPersistence.GetAllEventsByThemeAsync(theme, includeSpeakers);
                if (_events == null) return null;

                return _events;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            try
            {
                var _event = await _eventPersistence.GetEventByIdAsync(eventId, includeSpeakers);
                if (_event == null) return null;

                return _event;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}