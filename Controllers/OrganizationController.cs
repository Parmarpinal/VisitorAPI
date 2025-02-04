using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationRepository _organizationRepository;
        public OrganizationController(OrganizationRepository repository)
        {
            _organizationRepository = repository;
        }
        [HttpGet]
        public IActionResult GetOrganizations()
        {
            var organizations = _organizationRepository.SelectAll();
            return Ok(organizations);
        }

        [HttpPost]
        public IActionResult AddOrganization([FromBody] OrganizationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            bool isInserted = _organizationRepository.Add(model);
            if (isInserted)
                return Ok(new { MessageContent = "Organization added successfully" });
            return StatusCode(500, "An error occured");
        }

        [HttpPut("{id}")]
        public IActionResult EditOrganization(int id, [FromBody] OrganizationModel model)
        {
            if (model == null || id != model.OrganizationID)
            {
                return BadRequest();
            }

            bool isUpdated = _organizationRepository.Edit(model);
            if (!isUpdated)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganizationByID(int id)
        {
            var organization = _organizationRepository.SelectByPK(id);
            if (organization == null)
            {
                return NotFound();
            }
            return Ok(organization);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrganization(int id)
        {
            var isDeleted = _organizationRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
