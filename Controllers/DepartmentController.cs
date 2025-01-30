using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentController(DepartmentRepository repository)
        {
            _departmentRepository = repository;
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _departmentRepository.SelectAll();
            return Ok(departments);
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] DepartmentModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            bool isInserted = _departmentRepository.Add(model);
            if (isInserted)
                return Ok(new { MessageContent = "Department added successfully" });
            return StatusCode(500, "An error occured");
        }

        [HttpPut("{id}")]
        public IActionResult EditDepartment(int id, [FromBody] DepartmentModel model)
        {
            if (model == null || id != model.DepartmentID)
            {
                return BadRequest();
            }

            bool isUpdated = _departmentRepository.Edit(model);
            if (!isUpdated)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentByID(int id)
        {
            var department = _departmentRepository.SelectByPK(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var isDeleted = _departmentRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
