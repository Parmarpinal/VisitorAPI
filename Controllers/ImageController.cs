using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;
using VisitorWebAPI.Helpers;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private ImageRepository _repo;
        public ImageController(ImageRepository repository)
        {
            _repo = repository;
        }


        [HttpPost("upload")]
        public IActionResult uploadImage(IFormFile file)
        {
            string info = _repo.ImageUpload(file);
            return Ok(info);
        }
    }
}
