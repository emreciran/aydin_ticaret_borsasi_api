using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UsersResponse> GetAllUsers(int page, float limit)
        {
            return await _userRepository.GetAllUsers(page, limit);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}
