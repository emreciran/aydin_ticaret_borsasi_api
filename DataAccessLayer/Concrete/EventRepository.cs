using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext db;

        public EventRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<EventResponse> GetAllEvent(int page, float limit)
        {
            if (db.Events == null)
                return null;

            var pageResults = limit;
            var pageCount = Math.Ceiling(db.Events.Count() / pageResults);

            var events = await db.Events
                .OrderByDescending(x => x.ID)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var totalCount = db.Events.Count();

            var response = new EventResponse
            {
                Events = events,
                CurrentPage = page,
                Pages = (int)pageCount,
                Total = totalCount,
            };

            return response;
        }

        public async Task<Event> GetEventById(int id)
        {
            var model = await db.Events.FindAsync(id);
            if (model != null) return model;

            return null;
        }

        public async Task<Event> NewEvent(Event model)
        {
            db.Events.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public async Task<Event> UpdateEvent(Event model)
        {
            db.Events.Update(model);
            await db.SaveChangesAsync();
            return model;
        }

        public async Task DeleteEvent(int id)
        {
            var deletedActivity = await GetEventById(id);
            db.Events.Remove(deletedActivity);
            await db.SaveChangesAsync();
        }
    }
}
