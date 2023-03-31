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

        public async Task<User> GetUserById(int id)
        {
            var userDetails = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.USER_ID == id);

            if (userDetails == null) return null;

            return userDetails;
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
                CreatedDate = model.CreatedDate,
                Status = model.Status
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                db.Users.Add(userDetails);
                await db.SaveChangesAsync();

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
            var userData = await _userManager.FindByEmailAsync(user.Email);

            var userDetails = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == user.Email);

            var deletedRole = await _userManager.RemoveFromRoleAsync(userData, userDetails.Role);

            var result = await _userManager.AddToRoleAsync(userData, user.Role);

            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<UserManagerResponse> ChangeUserPassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Kullanıcı bulunamadı!",
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Mevcut şifreniz hatalı!"
                };
            }

            if(model.ConfirmPassword != model.NewPassword)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Onay şifreniz hatalı!"
                };
            }

            if (model.OldPassword == model.NewPassword)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Yeni şifreniz eski şifrenizden farklı olmalıdır"
                };
            }

            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Şifreniz başarıyla değiştirildi!"
            };
        }

        public async Task<User> UpdateUserInfo(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
    }
}
