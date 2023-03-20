using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAnnouncementService
    {
        Task<AnnouncementResponse> GetAllAnnouncement(int page, float limit);

        Task<Announcement> GetAnnouncementById(int id);

        Task<Announcement> NewAnnouncement(Announcement announcement);

        Task<Announcement> UpdateAnnouncement(Announcement announcement);

        Task DeleteAnnouncement(int id);
    }
}
