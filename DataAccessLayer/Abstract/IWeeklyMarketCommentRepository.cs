﻿using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IWeeklyMarketCommentRepository
    {
        Task<WeeklyMarketCommentResponse> GetAll(int page, float limit);

        Task<WeeklyMarketComment> GetById(int id);

        Task<WeeklyMarketComment> New(WeeklyMarketComment weeklyMarketComment);

        Task<WeeklyMarketComment> Update(WeeklyMarketComment weeklyMarketComment);

        Task Delete(int id);
    }
}
