using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private DashboardRepository _repo;

        public DashboardController(DashboardRepository repo)
        {
            _repo = repo;
        }

        [HttpGet()]
        public IActionResult GetDashboardData()
        {
            var data = _repo.GetData();
            return Ok(data);
        }
    }
}
