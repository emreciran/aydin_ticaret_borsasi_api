using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class WeeklyMarketCommentRepository : IWeeklyMarketCommentRepository
    {
        private readonly ApplicationDbContext db;

        public WeeklyMarketCommentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            var deletedData = await GetById(id);
            db.WeeklyMarketComments.Remove(deletedData);
            await db.SaveChangesAsync();
        }

        public async Task<WeeklyMarketCommentResponse> GetAll(int page, float limit)
        {
            if (db.WeeklyMarketComments == null)
                return null;

            var pageResults = limit;
            var pageCount = Math.Ceiling(db.WeeklyMarketComments.Count() / pageResults);

            var weeklyMarketComments = await db.WeeklyMarketComments
                .OrderByDescending(x => x.ID)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var totalCount = db.Announcements.Count();

            var response = new WeeklyMarketCommentResponse
            {
                WeeklyMarketComments = weeklyMarketComments,
                CurrentPage = page,
                Pages = (int)pageCount,
                Total = totalCount,
            };

            return response;
        }

        public async Task<WeeklyMarketComment> GetById(int id)
        {
            var weeklyMarketComment = await db.WeeklyMarketComments.FindAsync(id);
            if (weeklyMarketComment != null) return weeklyMarketComment;

            return null;
        }

        public async Task<WeeklyMarketComment> New(WeeklyMarketComment weeklyMarketComment)
        {
            db.WeeklyMarketComments.Add(weeklyMarketComment);
            await db.SaveChangesAsync();
            return weeklyMarketComment;
        }

        public async Task<WeeklyMarketComment> Update(WeeklyMarketComment weeklyMarketComment)
        {
            db.WeeklyMarketComments.Update(weeklyMarketComment);
            await db.SaveChangesAsync();
            return weeklyMarketComment;
        }
    }
}
