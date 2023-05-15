using Cassandra.Mapping.Attributes;
using GenericTools.Database;

namespace DirectMessages.Models
{
    public class DirectMessage : BaseEntity
    {
        [SecondaryIndex]
        public Guid ChannelId { get; set; }
        [Column("sender_id")]
        public Guid SenderId { get; set; }
        [Column("Message")]
        public string Message { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
