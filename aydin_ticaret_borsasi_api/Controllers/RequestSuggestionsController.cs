using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aydin_ticaret_borsasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RequestSuggestionsController : ControllerBase
    {
        private IRequestSuggestionService _requestSuggestionService;

        public RequestSuggestionsController(IRequestSuggestionService requestSuggestionService)
        {
            _requestSuggestionService = requestSuggestionService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int page = 1, float limit = 10)
        {
            var requestSuggestion = await _requestSuggestionService.GetAll(page, limit);
            if (requestSuggestion == null) return BadRequest();
            return Ok(requestSuggestion);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var requestSuggestion = await _requestSuggestionService.GetById(id);
            if (requestSuggestion == null) return BadRequest();
            return Ok(requestSuggestion);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NewRequestSuggestion(RequestSuggestion requestSuggestion)
        {
            var createdReqSugg = await _requestSuggestionService.NewRequestSuggestion(requestSuggestion);
            if (createdReqSugg == null) return BadRequest();
            return Ok(createdReqSugg);
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int id, bool status)
        {
            var updatedReqSugg = await _requestSuggestionService.UpdateStatus(id, status);
            if (updatedReqSugg == null) return BadRequest();
            return Ok(updatedReqSugg);
        }

        [HttpPut("Reply")]
        public async Task<IActionResult> ReplyRequestSuggestion(RequestSuggestion requestSuggestion)
        {
            var updatedReqSugg = await _requestSuggestionService.ReplyRequestSuggestion(requestSuggestion);
            if (updatedReqSugg == null) return BadRequest();
            return Ok(updatedReqSugg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestSuggestion(int id)
        {
            var reqSugg = await GetById(id);
            if (reqSugg != null)
            {
                await _requestSuggestionService.DeleteRequestSuggestion(id);
                return Ok("Silindi!");
            }

            return BadRequest();
        }
    }
}
