using DirectMessages.Models;
using DirectMessages.Models.DTO;
using DirectMessages.Repository;
using Guilds.Models.DTO;
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
            if (request.RecipientId.Count == 1)
            {
                var recipientList = request.RecipientId;
                recipientList.Add(request.SenderId);
                var groupDm = new DirectMessageChannel
                {
                    Id = Guid.NewGuid(),
                    Recipients = recipientList,
                    CreatedAt = DateTime.Now
                };
                _repository.Add(groupDm);
                return Ok(groupDm);
            } else if(request.RecipientId.Count > 1)
            {
                var recipientList = request.RecipientId;
                var groupDm = new DirectMessageChannel
                {
                    Id = Guid.NewGuid(),
                    Recipients = recipientList,
                    CreatedAt = DateTime.Now
                };
                _repository.Add(groupDm);
                return Ok(groupDm);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("createguildchannel")]
        public IActionResult CreateDirectMessage([FromBody] CreateGuildTextChannelDto request)
        {

            var recipientList = request.RecipientId;
            var groupDm = new DirectMessageChannel
            {
                Id = request.ChannelId,
                Recipients = recipientList,
                CreatedAt = DateTime.Now
            };
            _repository.Add(groupDm);
            return Ok(groupDm);
        }
        //GetAll
        [HttpGet]
        [Route("getalldmchannels")]
        public IActionResult GetAll()
        {
            var dmChannels = _repository.GetAll();
            return Ok(dmChannels);
        }
    }
}
