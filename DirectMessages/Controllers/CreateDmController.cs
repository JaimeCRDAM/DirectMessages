using DirectMessages.Models;
using DirectMessages.Models.DTO;
using DirectMessages.NetWorking;
using DirectMessages.Repository;
using Guilds.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DirectMessages.Controllers
{
    public class CreateDmController : ControllerBase
    {
        private IBaseRepository<DirectMessageChannel> _repository;
        private readonly IHttpApiRest _httpApiRest;

        public CreateDmController(
            IBaseRepository<DirectMessageChannel> repository,
            IHttpApiRest httpApiRest
            )
        {
            _repository = repository;
            _httpApiRest = httpApiRest;
        }
        [HttpPost]
        [Route("createdmchannel")]
        public IActionResult CreateDirectMessage([FromBody] CreateDmDto request)
        {
            if (request.RecipientId.Count == 1)
            {
                var notSender = request.RecipientId.Where(x => x != request.SenderId).First();
                string recipientName = "";
                var task = Task.Run(() => {
                    recipientName = _httpApiRest.GetUserNameById(notSender).Result;
                });
                task.Wait();
                var recipientList = request.RecipientId;
                recipientList.Add(request.SenderId);
                var groupDm = new DirectMessageChannel
                {
                    Id = Guid.NewGuid(),
                    Recipients = recipientList,
                    CreatedAt = DateTime.Now,
                    DirectedTo = recipientName
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
        //Get all dm channels that have a specific user
        [HttpGet]
        [Route("getdmchannels/{userId}")]
        public IActionResult GetDmChannels(Guid userId)
        {
            var dmChannels = _repository.GetAll().ToList().Where(x => x.Recipients.Contains(userId));
            return Ok(dmChannels);
        }
    }
}
