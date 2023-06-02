using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aydin_ticaret_borsasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeeklyMarketCommentsController : ControllerBase
    {
        private IWeeklyMarketCommentService _weeklyMarketCommentService;
        public static IWebHostEnvironment _webHostEnvironment;

        public WeeklyMarketCommentsController(IWeeklyMarketCommentService weeklyMarketCommentService, IWebHostEnvironment webHostEnvironment)
        {
            _weeklyMarketCommentService = weeklyMarketCommentService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int page = 1, float limit = 10)
        {
            var data = await _weeklyMarketCommentService.GetAll(page, limit);
            if (data == null) return BadRequest();
            return Ok(data);
        }

        //[HttpGet("{id}")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var data = await _weeklyMarketCommentService.GetById(id);
        //    if (data == null) return NotFound("Veri bulunamadı!");

        //    return Ok(data);
        //}

        [HttpGet("{type}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByType(string type)
        {
            var data = await _weeklyMarketCommentService.GetByType(type);
            if (data == null) return NotFound("Veri bulunamadı!");

            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> New([FromForm] WeeklyMarketComment weeklyMarketComment)
        {
            if (weeklyMarketComment.ImageFile != null)
            {
                weeklyMarketComment.ImageName = await SaveImage(weeklyMarketComment.ImageFile);
            }

            var created = await _weeklyMarketCommentService.New(weeklyMarketComment);
            if (created == null) return BadRequest();

            return CreatedAtAction("GetAll", new { id = created.ID }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] WeeklyMarketComment weeklyMarketComment)
        {
            if (weeklyMarketComment.ImageFile != null)
            {
                DeleteImage(weeklyMarketComment.ImageName);
                weeklyMarketComment.ImageName = await SaveImage(weeklyMarketComment.ImageFile);
            }

            var updated = await _weeklyMarketCommentService.Update(weeklyMarketComment);
            
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _weeklyMarketCommentService.GetById(id);

            if (data != null)
            {
                if (data.ImageName != null)
                {
                    DeleteImage(data.ImageName);
                }
                await _weeklyMarketCommentService.Delete(id);
                return Ok("Silindi");
            }

            return BadRequest();
        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile file)
        {
            string trustedFileNameForFileStorage;
            var untrustedFileName = new string(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');

            trustedFileNameForFileStorage = untrustedFileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/WeeklyMarket", trustedFileNameForFileStorage);

            await using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return trustedFileNameForFileStorage;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/WeeklyMarket", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
