namespace DirectMessages.Models.DTO
{
    public class CreateDmDto
    {
        public Guid SenderId { get; set; }
        public List<Guid> RecipientId { get; set; }
    }
}
