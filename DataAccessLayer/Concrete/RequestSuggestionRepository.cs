using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class RequestSuggestionRepository : IRequestSuggestionRepository
    {
        private readonly ApplicationDbContext db;
        private IMailRepository _mailRepository;

        public RequestSuggestionRepository(ApplicationDbContext db, IMailRepository mailRepository)
        {
            this.db = db;
            _mailRepository = mailRepository;
        }

        public async Task<RequestSuggestionResponse> GetAll(int page, float limit)
        {
            if (db.RequestSuggestions == null)
                return null;

            var pageResults = limit;
            var pageCount = Math.Ceiling(db.RequestSuggestions.Count() / pageResults);

            var requestSuggestion = await db.RequestSuggestions
                .OrderByDescending(x => x.ID)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var totalCount = db.RequestSuggestions.Count();

            var response = new RequestSuggestionResponse
            {
                RequestSuggestions = requestSuggestion,
                CurrentPage = page,
                Pages = (int)pageCount,
                Total = totalCount,
            };

            return response;
        }

        public async Task<RequestSuggestion> GetById(int id)
        {
            var requestSuggestion = await db.RequestSuggestions.FindAsync(id);
            if (requestSuggestion != null) return requestSuggestion;

            return null;
        }

        public async Task<RequestSuggestion> NewRequestSuggestion(RequestSuggestion requestSuggestion)
        {
            string subject = "Aydın Ticaret Borsası Talep/Öneri";
            string body = 
                $"<h1>Aydın Ticaret Borsası</h1>" +
                $"<p>Adı Soyadı: <span>{requestSuggestion.NameSurname}</span></p>" + 
                $"<p>Telefon: <span>{requestSuggestion.Phone}</span></p>" +
                $"<p>Email: <span>{requestSuggestion.Email}</span></p>" +
                $"<p>Mesaj: <span>{requestSuggestion.Message}</span></p>";

            await _mailRepository.SendEmailAsync("otogaming09@gmail.com", subject, body);

            db.RequestSuggestions.Add(requestSuggestion);
            await db.SaveChangesAsync();
            return requestSuggestion;
        }

        public async Task<RequestSuggestionResponse> SendRequestSuggestionEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestSuggestion> UpdateStatus(int id, bool status)
        {
            var updateReqSugg = await GetById(id);
            if (updateReqSugg != null)
            {
                updateReqSugg.Status = status;
                await db.SaveChangesAsync();
                return updateReqSugg;
            }

            return null;
        }
    }
}
