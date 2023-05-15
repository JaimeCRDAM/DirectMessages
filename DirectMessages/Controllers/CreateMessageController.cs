using DirectMessages.Models;
using DirectMessages.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DirectMessages.Controllers
{
    public class CreateMessageController : Controller
    {
        private IBaseRepository<DirectMessage> _repository;
        public CreateMessageController(IBaseRepository<DirectMessage> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("directmessages")]
        public IActionResult CreateDirectMessage([FromBody] DirectMessageDto request)
        {
            var dm = new DirectMessage
            {
                id = Guid.NewGuid(),
                SenderId = request.SenderId,
                ChannelId = request.ChannelId,
                Message = request.Message,
                CreatedAt = DateTime.Now
            };
            _repository.Add(dm);
            return Ok();
        }
    }
}
