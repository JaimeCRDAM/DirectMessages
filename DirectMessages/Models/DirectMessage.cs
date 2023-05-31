using Cassandra.Mapping.Attributes;
using DirectMessages.Models.DTO;
using GenericTools.Database;

namespace DirectMessages.Models
{
    public class DirectMessage : BaseEntity
    {
        [SecondaryIndex]
        public Guid ChannelId { get; set; }
        [Column("sender_id")]
        public User Sender { get; set; }
        [Column("Message")]
        public string Message { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public DirectMessageDto mapToDirectMessageDto()
        {
            return new DirectMessageDto
            {
                Sender = Sender,
                Message = Message,
                ChannelId = ChannelId,
                Id = Id
            };
        }
    }
}
