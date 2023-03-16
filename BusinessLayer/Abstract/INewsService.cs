﻿using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface INewsService
    {
        Task<List<News>> GetAllNews();

        Task<News> GetNewsById(int id);

        Task<News> CreateNews(News news);

        Task<News> UpdateNews(News news);

        Task DeleteNews(int id);
    }
}