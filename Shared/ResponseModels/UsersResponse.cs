using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseModels
{
    public class UsersResponse
    {
        public List<User> Users { get; set; } = new List<User>();

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public int Total { get; set; }
    }
}
