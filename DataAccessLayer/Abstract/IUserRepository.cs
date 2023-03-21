using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserRepository
    {
        Task<UsersResponse> GetAllUsers(int page, float limit);

        Task<User> UpdateUser(User user);
    }
}
