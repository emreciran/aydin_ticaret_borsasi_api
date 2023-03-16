using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsManager : INewsService
    {
        INewsRepository _newsRepository;

        public NewsManager(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task DeleteNews(int id)
        {
            await _newsRepository.DeleteNews(id);
        }

        public async Task<List<News>> GetAllNews()
        {
            return await _newsRepository.GetAllNews();
        }

        public async Task<News> GetNewsById(int id)
        {
            return await _newsRepository.GetNewsById(id);
        }

        public async Task<News> CreateNews(News news)
        {
            return await _newsRepository.CreateNews(news);
        }

        public async Task<News> UpdateNews(News news)
        {
            return await _newsRepository.UpdateNews(news);
        }
    }
}
