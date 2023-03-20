using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aydin_ticaret_borsasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsService _newsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(INewsService newsService, IWebHostEnvironment webHostEnvironment)
        {
            _newsService = newsService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews(int page, float limit)
        {
            var news = await _newsService.GetAllNews(page, limit);
            if (news == null) return BadRequest();

            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var news = await _newsService.GetNewsById(id);
            if (news == null) return NotFound("Haber bulunamadı");

            return Ok(news);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews([FromForm] News news)
        {
            if (news.ImageFile != null)
            {
                news.ImageName = await SaveImage(news.ImageFile);
            }

            var createdNews = await _newsService.CreateNews(news);
            if (createdNews == null) return BadRequest();

            return CreatedAtAction("GetAllNews", new { id = createdNews.ID }, createdNews);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNews(News news)
        {
            var updatedNews = await _newsService.UpdateNews(news);
            return Ok(updatedNews);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _newsService.GetNewsById(id);

            if (news != null)
            {
                DeleteImage(news.ImageName);
                await _newsService.DeleteNews(id);
                return Ok("Haber Silindi");
            }

            return BadRequest();
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
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
