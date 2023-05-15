using DirectMessages.Models;
using DirectMessages.Models.DTO;
using DirectMessages.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DirectMessages.Controllers
{
    public class CreateDmController : ControllerBase
    {
        private IBaseRepository<DirectMessageChannel> _repository;
        public CreateDmController(IBaseRepository<DirectMessageChannel> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("createdmchannel")]
        public IActionResult CreateDirectMessage([FromBody] CreateDmDto request)
        {
            var exist = _repository.Find(x => x.SenderId == request.SenderId && x.Recipients == request.RecipientId).FirstOrDefault();

            if (exist != null)
            {
                return BadRequest("Channel already created");
            }
            var dm = new DirectMessageChannel
            {
                id = Guid.NewGuid(),
                SenderId = request.SenderId,
                Recipients = request.RecipientId,
                CreatedAt = DateTime.Now
            };
            _repository.Add(dm);
            return Ok(dm);
        }
    }
}
