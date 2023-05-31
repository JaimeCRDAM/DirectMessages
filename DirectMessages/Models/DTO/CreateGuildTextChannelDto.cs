using System.Net;

namespace Guilds.Models.DTO
{
    public class CreateGuildTextChannelDto
    {
        public Guid ChannelId { get; set; }
        public List<Guid> RecipientId { get; set; }
    }
}
