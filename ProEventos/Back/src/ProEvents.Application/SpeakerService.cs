using System;
using System.Threading.Tasks;
using ProEvents.Application.Interfaces;
using ProEvents.Domain;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Application
{
    public class SpeakerService : ISpeakerService
    {
        private readonly IGenericPersistence _genericPersistence;
        private readonly ISpeakerPersistence _speakerPersistence;

        public SpeakerService(IGenericPersistence genericPersistence, ISpeakerPersistence speakerPersistence)
        {
            this._speakerPersistence = speakerPersistence;
            this._genericPersistence = genericPersistence;

        }

        public async Task<Speaker> AddSpeaker(Speaker model)
        {
            try
            {
                _genericPersistence.Add<Speaker>(model);
                if (await _genericPersistence.SaveChangesAsync())
                {
                    return await _speakerPersistence.GetSpeakerByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Speaker> UpdateSpeaker(int speakerId, Speaker model)
        {
            try
            {
                var _eventToUpdate = await _speakerPersistence.GetSpeakerByIdAsync(speakerId);
                if (_eventToUpdate == null) return null;

                model.Id = speakerId;

                _genericPersistence.Update<Speaker>(model);
                if (await _genericPersistence.SaveChangesAsync())
                {
                    return await _speakerPersistence.GetSpeakerByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> DeleteSpeaker(int speakerId)
        {
            try
            {
                var _eventToDelete = await _speakerPersistence.GetSpeakerByIdAsync(speakerId);
                if (_eventToDelete == null) throw new Exception($"Speaker to delete not found. ");

                _genericPersistence.Delete<Speaker>(_eventToDelete);
                return await _genericPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false)
        {
            try
            {
                var _speakers = await _speakerPersistence.GetAllSpeakersAsync();
                if (_speakers == null) return null;

                return _speakers;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false)
        {
            try
            {
                var _speakersByName = await _speakerPersistence.GetAllSpeakersByNameAsync(name, includeEvents);
                if (_speakersByName == null) return null;

                return _speakersByName;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false)
        {
            try
            {
                var _speaker = await _speakerPersistence.GetSpeakerByIdAsync(speakerId, includeEvents);
                if (_speaker == null) return null;

                return _speaker;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}