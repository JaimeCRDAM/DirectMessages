using GenericTools.Database;

namespace DirectMessages.Models
{
    public class DirectMessageChannel : BaseEntity
    {
        public List<Guid> Recipients { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DirectedTo { get; set; }
    }
}
