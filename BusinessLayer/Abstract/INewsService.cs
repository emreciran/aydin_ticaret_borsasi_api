﻿using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface INewsService
    {
        Task<NewsResponse> GetAllNews(int page, float limit);

        Task<News> GetNewsById(int id);

        Task<News> CreateNews(News news);

        Task<News> UpdateNews(News news);

        Task DeleteNews(int id);
    }
}
