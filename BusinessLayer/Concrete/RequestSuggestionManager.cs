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
    public class RequestSuggestionManager : IRequestSuggestionService
    {
        IRequestSuggestionRepository _requestSuggestionRepository;

        public RequestSuggestionManager(IRequestSuggestionRepository requestSuggestionRepository)
        {
            _requestSuggestionRepository = requestSuggestionRepository;
        }

        public async Task DeleteRequestSuggestion(int id)
        {
            await _requestSuggestionRepository.DeleteRequestSuggestion(id);
        }

        public async Task<RequestSuggestionResponse> GetAll(int page, float limit)
        {
            return await _requestSuggestionRepository.GetAll(page, limit);
        }

        public async Task<RequestSuggestion> GetById(int id)
        {
            return await _requestSuggestionRepository.GetById(id);
        }

        public async Task<RequestSuggestion> NewRequestSuggestion(RequestSuggestion requestSuggestion)
        {
            return await _requestSuggestionRepository.NewRequestSuggestion(requestSuggestion);
        }

        public async Task<RequestSuggestion> ReplyRequestSuggestion(RequestSuggestion requestSuggestion)
        {
            return await _requestSuggestionRepository.ReplyRequestSuggestion(requestSuggestion);
        }

        public async Task<RequestSuggestion> UpdateStatus(int id, bool status)
        {
            return await _requestSuggestionRepository.UpdateStatus(id, status);
        }
    }
}
