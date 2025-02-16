using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _repo;

        public UserController(UserRepository repo)
        {
            _repo = repo;
        }

        //[HttpGet]
        //public IActionResult GetUsers(int? organizationID, int? departmentID, int? userTypeID)
        //{
        //    var users = _repo.SelectAll(organizationID, departmentID, userTypeID);
        //    return Ok(users);
        //}
        [HttpGet("search")]
        public IActionResult GetUsers([FromQuery] int? orgID, [FromQuery] int? deptID, [FromQuery] int? typeID)
        {
            var users = _repo.SelectAll(orgID, deptID, typeID);
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            bool isInserted = _repo.Add(model);
            if (isInserted)
                return Ok(new { MessageContent = "User added successfully" });
            return StatusCode(500, "An error occured");
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, [FromBody] UserModel model)
        {
            if (model == null || id != model.UserID)
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

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] UserLoginModel model)
        {
            var user = _repo.UserLogin(model);
            if(user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(user);
        }
    }
}
