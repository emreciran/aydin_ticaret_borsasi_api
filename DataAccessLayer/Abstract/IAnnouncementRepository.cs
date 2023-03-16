using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IAnnouncementRepository
    {
        Task<List<Announcement>> GetAllAnnouncement();

        Task<Announcement> GetAnnouncementById(int id);

        Task<Announcement> NewAnnouncement(Announcement announcement);

        Task<Announcement> UpdateAnnouncement(Announcement announcement);

        Task DeleteAnnouncement(int id);
    }
}
