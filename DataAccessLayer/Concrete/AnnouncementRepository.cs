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
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext db;

        public AnnouncementRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<AnnouncementResponse> GetAllAnnouncement(int page, float limit)
        {
            if (db.Announcements == null)
                return null;

            var pageResults = limit;
            var pageCount = Math.Ceiling(db.Announcements.Count() / pageResults);

            var announcements = await db.Announcements
                .OrderByDescending(x => x.ID)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var totalCount = db.Announcements.Count();

            var response = new AnnouncementResponse
            {
                Announcements = announcements,
                CurrentPage = page,
                Pages = (int)pageCount,
                Total = totalCount,
            };

            return response;
        }

        public async Task<Announcement> GetAnnouncementById(int id)
        {
            var announcement = await db.Announcements.FindAsync(id);
            if(announcement != null) return announcement;

            return null;
        }

        public async Task<Announcement> NewAnnouncement(Announcement announcement)
        {
            db.Announcements.Add(announcement);
            await db.SaveChangesAsync();
            return announcement;
        }

        public async Task<Announcement> UpdateAnnouncement(Announcement announcement)
        {
            db.Announcements.Update(announcement);
            await db.SaveChangesAsync();
            return announcement;
        }

        public async Task DeleteAnnouncement(int id)
        {
            var deletedAnnouncement = await GetAnnouncementById(id);
            db.Announcements.Remove(deletedAnnouncement);
            await db.SaveChangesAsync();
        }
    }
}
