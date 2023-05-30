using GenericTools.Database;

namespace DirectMessages.Models
{
    public class DirectMessageChannel : BaseEntity
    {
        public IEnumerable<Guid> Recipients { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
