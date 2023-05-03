using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRequestSuggestionService
    {
        Task<RequestSuggestionResponse> GetAll(int page, float limit);

        Task<RequestSuggestion> GetById(int id);

        Task<RequestSuggestion> NewRequestSuggestion(RequestSuggestion requestSuggestion);

        Task<RequestSuggestion> UpdateStatus(int id, bool status);

        Task<RequestSuggestion> ReplyRequestSuggestion(RequestSuggestion requestSuggestion);

        Task DeleteRequestSuggestion(int id);
    }
}
