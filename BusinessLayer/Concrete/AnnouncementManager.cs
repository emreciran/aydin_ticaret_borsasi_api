using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AnnouncementManager : IAnnouncementService
    {
        IAnnouncementRepository _announcementRepository;

        public AnnouncementManager(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task DeleteAnnouncement(int id)
        {   
            await _announcementRepository.DeleteAnnouncement(id);
        }

        public async Task<List<Announcement>> GetAllAnnouncement()
        {
            return await _announcementRepository.GetAllAnnouncement();
        }

        public async Task<Announcement> GetAnnouncementById(int id)
        {
            return await _announcementRepository.GetAnnouncementById(id);
        }

        public async Task<Announcement> NewAnnouncement(Announcement announcement)
        {
            return await _announcementRepository.NewAnnouncement(announcement);
        }

        public async Task<Announcement> UpdateAnnouncement(Announcement announcement)
        {
            return await _announcementRepository.UpdateAnnouncement(announcement);
        }
    }
}
