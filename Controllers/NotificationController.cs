using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorWebAPI.Data;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationRepository _notificationRepository;
        public NotificationController(NotificationRepository repository)
        {
            _notificationRepository = repository;
        }
        [HttpGet("[action]/{id}")]
        public IActionResult GetNotificationsByHostID(int id)
        {
            var noti = _notificationRepository.SelectByHostID(id);
            return Ok(noti);
        }
        [HttpPost]
        public IActionResult AddNotification([FromBody] NotificationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            bool isInserted = _notificationRepository.Add(model);
            if (isInserted)
                return Ok(new { MessageContent = "Notification added successfully" });
            return StatusCode(500, "An error occured");
        }

        [HttpPut("{id}")]
        public IActionResult EditNotification(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            bool isUpdated = _notificationRepository.Edit(id);
            if (!isUpdated)
                return NotFound();
            return NoContent();
        }
    }
}
