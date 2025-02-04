using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorRepository _visitorRepository;
        public VisitorController(VisitorRepository repo)
        {
            _visitorRepository = repo;
        }
        [HttpGet]
        public IActionResult GetVisitors()
        {
            var visitors = _visitorRepository.SelectAll();
            return Ok(visitors);
        }

        [HttpPost]
        public IActionResult AddVisitor([FromBody] VisitorModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            bool isInserted = _visitorRepository.Add(model);
            if (isInserted)
                return Ok(new { MessageContent = "Visitor added successfully" });
            return StatusCode(500, "An error occured");
        }

        [HttpPut("{id}")]
        public IActionResult EditVisitor(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            bool isUpdated = _visitorRepository.EditToCheckedOut(id);
            if (!isUpdated)
                return StatusCode(500, new { message = "An error occurred."});
            return Ok(new { message = "Visitor checked out successfully." }); ;
        }

        [HttpGet("{id}")]
        public IActionResult GetVisitorByID(int id)
        {
            var visitor = _visitorRepository.SelectByPK(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return Ok(visitor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVisitor(int id)
        {
            var isDeleted = _visitorRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("GetByUserID/{id}")]
        public IActionResult GetVisitorByUserID(int id)
        {
            var visitor = _visitorRepository.SelectByUserID(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return Ok(visitor);
        }
    }
}
