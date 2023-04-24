using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IEventRepository
    {
        Task<EventResponse> GetAllEvent(int page, float limit);

        Task<Event> GetEventById(int id);

        Task<Event> NewEvent(Event model);

        Task<Event> UpdateEvent(Event model);

        Task DeleteEvent(int id);
    }
}
