using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private UserTypeRepository _repo;

        public UserTypeController(UserTypeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _repo.SelectAll();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserTypeModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            bool isInserted = _repo.Add(model);
            if (isInserted)
                return Ok(new { MessageContent = "User type added successfully" });
            return StatusCode(500, "An error occured");
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, [FromBody] UserTypeModel model)
        {
            if (model == null || id != model.UserTypeID)
            {
                return BadRequest();
            }

            bool isUpdated = _repo.Edit(model);
            if (!isUpdated)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var User = _repo.SelectByPK(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var isDeleted = _repo.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
