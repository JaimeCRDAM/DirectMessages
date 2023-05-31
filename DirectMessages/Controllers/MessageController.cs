
using DirectMessages.Models;
using DirectMessages.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DirectMessages.Controllers
{
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IBaseRepository<DirectMessage> _DMrepository;
        private readonly IBaseRepository<DirectMessageChannel> _DMCrepository;

        public MessageController(
            IBaseRepository<DirectMessage> DMrepository,
            IBaseRepository<DirectMessageChannel> DMCrepository
            )
        {
            _DMrepository = DMrepository;
            _DMCrepository = DMCrepository;
        }

        [HttpPost]
        [Route("directmessages")]
        public IActionResult CreateDirectMessage([FromBody] DirectMessageDto request)
        {
            var dm = new DirectMessage
            {
                Id = Guid.NewGuid(),
                Sender = request.Sender,
                ChannelId = request.ChannelId,
                Message = request.Message,
                CreatedAt = DateTime.Now
            };
            _DMrepository.Add(dm);
            return Ok(dm.mapToDirectMessageDto());
        }

        [HttpGet]
        [Route("directmessages/{channelId}")]
        public IActionResult GetDirectMessages(Guid channelId)
        {
            var messages = _DMrepository.Find(dm => dm.ChannelId == channelId).OrderByDescending(dm => dm.CreatedAt).Take(50).ToList();
            var messageDtos = new List<DirectMessageDto>();
            foreach (var message in messages)
            {
                messageDtos.Add(message.mapToDirectMessageDto());
            }
            return Ok(messageDtos);
        }
    }
}
