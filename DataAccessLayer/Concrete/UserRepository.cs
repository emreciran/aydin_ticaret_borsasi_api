using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared.ResponseModels;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;
        private UserManager<IdentityUser> _userManager;

        public UserRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            _userManager = userManager;
        }

        public async Task<UsersResponse> GetAllUsers(int page, float limit)
        {
            if (db.Announcements == null)
                return null;

            var pageResult = limit;
            var pageCount = Math.Ceiling(db.Users.Count() / pageResult);

            var users = await db.Users
                .OrderByDescending(x => x.USER_ID)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();

            var totalCount = db.Users.Count();

            var response = new UsersResponse
            {
                Users = users,
                CurrentPage = page,
                Pages = (int)pageCount,
                Total = totalCount
            };

            return response;
        }

        public async Task<UserManagerResponse> CreateUser(CreateUserViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Model is null!");

            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            var existingUsername = await _userManager.FindByNameAsync(model.Username);

            if (existingEmail != null)
            {
                return new UserManagerResponse
                {
                    Message = "Bu email zaten kullanılıyor!",
                    IsSuccess = false
                };
            }

            if (existingUsername != null)
            {
                return new UserManagerResponse
                {
                    Message = "Bu kullanıcı adı zaten kullanılıyor!",
                    IsSuccess = false
                };
            }

            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username
            };

            var userDetails = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Username = model.Username,
                Role = model.Role,
                CreatedDate = DateTime.Now,
                Status = model.Status
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                db.Users.Add(userDetails);
                await db.SaveChangesAsync();

                var userRoles = await _userManager.GetRolesAsync(user);

                return new UserManagerResponse
                {
                    Message = "Kullanıcı başarıyla oluşturuldu",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "Kullanıcı oluşturulamadı!",
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        public async Task<User> UpdateUser(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
    }
}
