using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using Shared.ViewModels;
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

        public async Task<UserManagerResponse> ChangeUserPassword(ChangePasswordViewModel model)
        {
            return await _userRepository.ChangeUserPassword(model);
        }

        public async Task<UserManagerResponse> CreateUser(CreateUserViewModel model)
        {
            return await _userRepository.CreateUser(model);
        }

        public async Task<UsersResponse> GetAllUsers(int page, float limit)
        {
            return await _userRepository.GetAllUsers(page, limit);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task<User> UpdateUserInfo(User user)
        {
            return await _userRepository.UpdateUserInfo(user);
        }
    }
}
