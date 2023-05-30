
using DirectMessages.Models;
using DirectMessages.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DirectMessages.Controllers
{
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IBaseRepository<DirectMessage> _repository;

        public MessageController(
            IBaseRepository<DirectMessage> repository
            )
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("directmessages")]
        public IActionResult CreateDirectMessage([FromBody] DirectMessageDto request)
        {
           
            var dm = new DirectMessage
            {
                Id = Guid.NewGuid(),
                SenderId = request.SenderId,
                ChannelId = request.ChannelId,
                Message = request.Message,
                CreatedAt = DateTime.Now
            };
            _repository.Add(dm);
            return Ok(dm);
        }
    }
}
