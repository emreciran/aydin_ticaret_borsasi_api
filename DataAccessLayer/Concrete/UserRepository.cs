using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared.ResponseModels;
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

        public UserRepository(ApplicationDbContext db)
        {
            this.db = db;
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

        public async Task<User> UpdateUser(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
    }
}
