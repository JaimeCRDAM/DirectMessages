using GenericTools.Database;

namespace DirectMessages.Models
{
    public class DirectMessageChannel : BaseEntity
    {
        public List<Guid> Recipients { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DirectedTo { get; set; }

        public Dictionary<string, string> mapToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "id", Id.ToString() },
                { "recipients", Recipients.ToString() },
                { "created_at", CreatedAt.ToString() },
                { "directed_to", DirectedTo }
            };
        }
    }
}
