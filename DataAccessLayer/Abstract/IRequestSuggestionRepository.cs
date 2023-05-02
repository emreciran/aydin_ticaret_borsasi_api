using EntitiesLayer.Concrete;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRequestSuggestionRepository
    {
        Task<RequestSuggestionResponse> SendRequestSuggestionEmail(string email);

        Task<RequestSuggestionResponse> GetAll(int page, float limit);

        Task<RequestSuggestion> GetById(int id);

        Task<RequestSuggestion> NewRequestSuggestion(RequestSuggestion requestSuggestion);

        Task<RequestSuggestion> UpdateStatus(int id, bool status);
    }
}
