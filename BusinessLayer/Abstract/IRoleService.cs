using Microsoft.AspNetCore.Identity;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllRoles();

        Task<RoleManagerResponse> CreateRole(string roleName);

        Task<RoleManagerResponse> AddUserToRole(string email, string roleName);

        Task<IList<string>> GetUserRoles(string email);

        Task<RoleManagerResponse> RemoveUserFromRole(string email, string roleName);

        Task<RoleManagerResponse> UpdateUserFromRole(string email, string currentRoleName, string newRoleName);
    }
}
