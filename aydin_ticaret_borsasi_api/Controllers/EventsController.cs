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
    public class EventsController : ControllerBase
    {
        private IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEvent(int page = 1, float limit = 10)
        {
            var events = await _eventService.GetAllEvent(page, limit);
            if (events == null) return BadRequest();
            return Ok(events);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventData = await _eventService.GetEventById(id);
            if (eventData == null) return BadRequest();
            return Ok(eventData);
        }

        [HttpPost]
        public async Task<IActionResult> NewEvent(Event model)
        {
            var createdEvent = await _eventService.NewEvent(model);
            if (createdEvent == null) return BadRequest();
            return Ok(createdEvent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(Event model)
        {
            var updatedEvent = await _eventService.UpdateEvent(model);
            if (updatedEvent == null) return BadRequest();
            return Ok(updatedEvent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventData = await _eventService.GetEventById(id);

            if (eventData != null)
            {
                await _eventService.DeleteEvent(id);
                return Ok("Etkinlik silindi");
            }

            return BadRequest();
        }
    }
}
