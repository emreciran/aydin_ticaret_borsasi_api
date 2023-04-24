using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class EventManager : IEventService
    {
        IEventRepository _eventRepository;

        public EventManager(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task DeleteEvent(int id)
        {
            await _eventRepository.DeleteEvent(id);
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _eventRepository.GetEventById(id);
        }

        public async Task<EventResponse> GetAllEvent(int page, float limit)
        {
            return await _eventRepository.GetAllEvent(page, limit);
        }

        public async Task<Event> NewEvent(Event model)
        {
            return await _eventRepository.NewEvent(model);
        }

        public async Task<Event> UpdateEvent(Event model)
        {
            return await _eventRepository.UpdateEvent(model);
        }
    }
}
