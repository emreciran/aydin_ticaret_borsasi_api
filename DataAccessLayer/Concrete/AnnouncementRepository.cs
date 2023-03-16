using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Announcement>> GetAllAnnouncement()
        {
            return await db.Announcements.ToListAsync();
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
