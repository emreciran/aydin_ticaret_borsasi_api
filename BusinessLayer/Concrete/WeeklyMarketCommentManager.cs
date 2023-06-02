using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WeeklyMarketCommentManager : IWeeklyMarketCommentService
    {
        IWeeklyMarketCommentRepository _weeklyMarketCommentRepository;

        public WeeklyMarketCommentManager(IWeeklyMarketCommentRepository weeklyMarketCommentRepository)
        {
            _weeklyMarketCommentRepository = weeklyMarketCommentRepository;
        }

        public async Task Delete(int id)
        {
            await _weeklyMarketCommentRepository.Delete(id);
        }

        public async Task<WeeklyMarketCommentResponse> GetAll(int page, float limit)
        {
            return await _weeklyMarketCommentRepository.GetAll(page, limit);
        }

        public async Task<WeeklyMarketComment> GetById(int id)
        {
            return await _weeklyMarketCommentRepository.GetById(id);
        }

        public async Task<WeeklyMarketComment> GetByType(string type)
        {
            return await _weeklyMarketCommentRepository.GetByType(type);
        }

        public async Task<WeeklyMarketComment> New(WeeklyMarketComment weeklyMarketComment)
        {
            return await _weeklyMarketCommentRepository.New(weeklyMarketComment);
        }

        public async Task<WeeklyMarketComment> Update(WeeklyMarketComment weeklyMarketComment)
        {
            return await _weeklyMarketCommentRepository.Update(weeklyMarketComment);
        }
    }
}
