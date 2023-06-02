
using DirectMessages.Models;
using DirectMessages.Models.DTO;
using DirectMessages.NetWorking;
using GenericTools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Channels;

namespace DirectMessages.Controllers
{
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IBaseRepository<DirectMessage> _DMrepository;
        private readonly IBaseRepository<DirectMessageChannel> _DMCrepository;
        private readonly IHttpApiRest _httpApiRest;
        private readonly FireBase _firebase;

        public MessageController(
            IBaseRepository<DirectMessage> DMrepository,
            IBaseRepository<DirectMessageChannel> DMCrepository,
            IHttpApiRest httpApiRest,
            FireBase firebase
            )
        {
            _DMrepository = DMrepository;
            _DMCrepository = DMCrepository;
            _httpApiRest = httpApiRest;
            _firebase = firebase;
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
            string json = JsonConvert.SerializeObject(dm);
            _firebase.PublishToTopic(
                new Dictionary<string, string>
                {
                    {"DirectMessage", json }
                },
                dm.ChannelId.ToString()
            );
            return Ok(dm.mapToDirectMessageDto());
        }

        [HttpGet]
        [Route("directmessages/{channelId}")]
        public IActionResult GetDirectMessages(Guid channelId)
        {
            var messages = _DMrepository.Find(dm => dm.ChannelId == channelId).OrderBy(dm => dm.CreatedAt).Take(50).ToList();
            var messageDtos = new List<DirectMessageDto>();
            foreach (var message in messages)
            {
                messageDtos.Add(message.mapToDirectMessageDto());
            }
            return Ok(messageDtos);
        }
    }
}
