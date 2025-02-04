using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly DropDownRepository _dropDownRepository;
        public DropDownController(DropDownRepository repository)
        {
            _dropDownRepository = repository;
        }
        [HttpGet("usertypes")]
        public IActionResult GetUserTypes()
        {
            var users = _dropDownRepository.SelectAllUserType();
            return Ok(users);
        }

        [HttpGet("organizations")]
        public IActionResult GetOrganizations()
        {
            var organization = _dropDownRepository.SelectAllOrganization();
            return Ok(organization);
        }

        [HttpGet("departments/{id}")]
        public IActionResult GetDepartments(int id)
        {
            var department = _dropDownRepository.SelectAllDepartment(id);
            return Ok(department);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _dropDownRepository.SelectAllUser();
            return Ok(users);
        }

        [HttpGet("hosts/{id}")]
        public IActionResult GetHosts(int id)
        {
            var hosts = _dropDownRepository.SelectAllHost(id);
            return Ok(hosts);
        }
    }
}
