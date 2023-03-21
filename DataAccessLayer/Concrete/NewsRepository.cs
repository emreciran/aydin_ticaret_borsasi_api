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
    public class NewsRepository : INewsRepository
    {
        private ApplicationDbContext db;

        public NewsRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task DeleteNews(int id)
        {
            var deletedNews = await GetNewsById(id);
            db.News.Remove(deletedNews);
            await db.SaveChangesAsync();
        }

        public async Task<NewsResponse> GetAllNews(int page, float limit)
        {
            if (db.News == null)
            {
                return new NewsResponse
                {
                    News = null,
                };
            }

            var pageResult = limit;
            var pageCount = Math.Ceiling(db.News.Count() / pageResult);

            var news = await db.News
                .OrderByDescending(x => x.ID)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();

            var totalCount = db.News.Count();

            var response = new NewsResponse
            {
                News = news,
                CurrentPage = page,
                Pages = (int)pageCount,
                Total = totalCount
            };

            return response;
        }

        public async Task<News> GetNewsById(int id)
        {
            var news = await db.News.FindAsync(id);
            if (news != null) return news;

            return null;
        }

        public async Task<News> CreateNews(News news)
        {
            db.News.Add(news);
            await db.SaveChangesAsync();
            return news;
        }

        public async Task<News> UpdateNews(News news)
        {
            db.News.Update(news);
            await db.SaveChangesAsync();
            return news;
        }
    }
}
