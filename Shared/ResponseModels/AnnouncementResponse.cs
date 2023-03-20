using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseModels
{
    public class AnnouncementResponse
    {
        public List<Announcement> Announcements { get; set; } = new List<Announcement>();

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public int Total { get; set; }

    }
}
