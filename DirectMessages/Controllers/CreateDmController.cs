using DirectMessages.Models;
using DirectMessages.Models.DTO;
using DirectMessages.NetWorking;
using DirectMessages.Repository;
using GenericTools;
using Guilds.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace DirectMessages.Controllers
{
    public class CreateDmController : ControllerBase
    {
        private IBaseRepository<DirectMessageChannel> _repository;
        private readonly IHttpApiRest _httpApiRest;
        private readonly FireBase _firebase;

        public CreateDmController(
            IBaseRepository<DirectMessageChannel> repository,
            IHttpApiRest httpApiRest,
            FireBase firebase
            )
        {
            _repository = repository;
            _httpApiRest = httpApiRest;
            _firebase = firebase;
        }
        [HttpPost]
        [Route("createdmchannel")]
        public IActionResult CreateDirectMessage([FromBody] CreateDmDto request)
        {
            if (request.RecipientId.Count == 1)
            {
                var notSender = request.RecipientId.Where(x => x != request.SenderId).First();
                string recipientName = "";
                Task.Run(() => {
                    recipientName = _httpApiRest.GetUserNameById(notSender).Result;
                }).Wait();
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
                Task.Run(async () => {
                    var lol = await _httpApiRest.SubscribeToChannel(recipientList, groupDm.Id);
                    string json = JsonConvert.SerializeObject(groupDm.mapToDictionary());
                    _firebase.PublishToTopic(
                        new Dictionary<string, string>
                        {
                        {"DirectMessageChannel",  json}
                        },
                        groupDm.Id.ToString()
                    );
                });

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
                _httpApiRest.SubscribeToChannel(recipientList, groupDm.Id);
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
            _httpApiRest.SubscribeToChannel(recipientList, groupDm.Id);
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
