﻿using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task<UsersResponse> GetAllUsers(int page, float limit);

        Task<UserManagerResponse> CreateUser(CreateUserViewModel model);

        Task<User> UpdateUser(User user);
    }
}
