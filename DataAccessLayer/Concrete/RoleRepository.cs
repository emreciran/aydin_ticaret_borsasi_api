using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext db;

        public RoleRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.db = db;
        }

        public async Task<RoleManagerResponse> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new RoleManagerResponse
                {
                    Message = $"{email} Mail adresine ait kullanıcı bulunamadı",
                    IsSuccess = false
                };
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);   

            if (!roleExist)
            {
                return new RoleManagerResponse
                {
                    Message = "Böyle bir rol bulunamadı!",
                    IsSuccess = false
                };
            }

            var currentRole = await GetUserRoles(email);

            if (currentRole.Count > 0)
            {
                var deleteCurrentRole = await RemoveUserFromRole(email, currentRole[0]);
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            var userDetails = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
            userDetails.Role = roleName;
            db.Update(userDetails);
            await db.SaveChangesAsync();

            if (result.Succeeded)
            {
                return new RoleManagerResponse
                {
                    Message = $"{email} Mail adresine ait kullanıcı {currentRole[0]} rolünden kaldırıldı, {roleName} rolüne eklendi.",
                    IsSuccess = true
                };
            }

            return new RoleManagerResponse
            {
                Message = "Kullanıcı role eklenemedi!",
                IsSuccess = false
            };
        }

        public async Task<RoleManagerResponse> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (result.Succeeded)
                {
                    return new RoleManagerResponse
                    {
                        Message = $"{roleName} adlı rol başarıyla eklenmiştir.",
                        IsSuccess = true
                    };
                }

                return new RoleManagerResponse
                {
                    Message = "Role eklenemedi!!",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new RoleManagerResponse
            {
                Message = "Bu rol zaten eklenmiş!",
                IsSuccess = false
            };
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (!roles.Any()) return null;

            return roles;
        }

        public async Task<IList<string>> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<RoleManagerResponse> RemoveUserFromRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new RoleManagerResponse
                {
                    Message = $"{email} mail adresine ait kullanıcı bulunamadı.",
                    IsSuccess = false
                };
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if(!roleExist)
            {
                return new RoleManagerResponse
                {
                    Message = "Böyle bir rol bulunamadı!",
                    IsSuccess = false
                };
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return new RoleManagerResponse
                {
                    Message = $"{email} Mail adresine ait kullanıcıdan {roleName} rolü kaldırıldı",
                    IsSuccess = true
                };
            }

            return new RoleManagerResponse
            {
                Message = "Rol kaldırılamadı!",
                IsSuccess = false
            };
        }

        public async Task<RoleManagerResponse> UpdateUserFromRole(string email, string currentRoleName, string newRoleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new RoleManagerResponse
                {
                    Message = $"{email} Mail adresine ait kullanıcı bulunamadı",
                    IsSuccess = false
                };
            }

            var roleExistCurrent = await _roleManager.RoleExistsAsync(currentRoleName);
            var roleExistNew = await _roleManager.RoleExistsAsync(newRoleName);

            if (!roleExistCurrent)
            {
                return new RoleManagerResponse
                {
                    Message = $"{currentRoleName} Böyle bir rol bulunamadı!",
                    IsSuccess = false
                };
            }

            if (!roleExistNew)
            {
                return new RoleManagerResponse
                {
                    Message = $"{newRoleName} Böyle bir rol bulunamadı!",
                    IsSuccess = false
                };
            }

            var deletedRole = await _userManager.RemoveFromRoleAsync(user, currentRoleName);

            var result = await _userManager.AddToRoleAsync(user, newRoleName);

            if (result.Succeeded)
            {
                return new RoleManagerResponse
                {
                    Message = $"{email} Mail adresine ait kullanıcı {currentRoleName} rolünden kaldırılıp {newRoleName} rolüne eklendi.",
                    IsSuccess = true
                };
            }

            return new RoleManagerResponse
            {
                Message = "Rol güncellenemedi!",
                IsSuccess = false
            };
        }
    }
}
