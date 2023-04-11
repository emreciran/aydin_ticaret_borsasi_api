using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace aydin_ticaret_borsasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnnouncementsController : ControllerBase
    {
        private IAnnouncementService _announcementService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AnnouncementsController(IAnnouncementService announcementService, IWebHostEnvironment webHostEnvironment)
        {
            _announcementService = announcementService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAnnouncement(int page, float limit)
        {
            var announcement = await _announcementService.GetAllAnnouncement(page, limit);
            if (announcement == null) return BadRequest();
            return Ok(announcement);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            var announcement = await _announcementService.GetAnnouncementById(id);
            if (announcement == null) return NotFound("Duyuru bulunamadı!");

            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> NewAnnouncement([FromForm] Announcement announcement)
        {
            if (announcement.ImageFile != null)
            {
                announcement.ImageName = await SaveImage2(announcement.ImageFile);
            }

            var createdAnnouncement = await _announcementService.NewAnnouncement(announcement);
            if (createdAnnouncement == null) return BadRequest();

            return CreatedAtAction("GetAllAnnouncement", new { id = createdAnnouncement.ID }, createdAnnouncement);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncement([FromForm]Announcement announcement)
        {
            if (announcement.ImageFile != null)
            {
                announcement.ImageName = await SaveImage(announcement.ImageFile);
            }

            var updatedAnnouncement = await _announcementService.UpdateAnnouncement(announcement);
            return Ok(updatedAnnouncement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = await _announcementService.GetAnnouncementById(id);

            if (announcement != null)
            {
                if (announcement.ImageName != null)
                {
                    DeleteImage(announcement.ImageName);
                }
                await _announcementService.DeleteAnnouncement(id);
                return Ok("Duyuru silindi");
            }

            return BadRequest("Duyuru silindi!");
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile file)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return imageName;
        }
        [NonAction]
        public async Task<string> SaveImage2(IFormFile file)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Announcements", file.FileName);

     
            using (var stream = System.IO.File.Create(path))
            {
                await file.CopyToAsync(stream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
