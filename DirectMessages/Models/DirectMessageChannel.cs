using GenericTools.Database;

namespace DirectMessages.Models
{
    public class DirectMessageChannel : BaseEntity
    {
        public Guid Recipients { get; set; }
        public Guid SenderId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
