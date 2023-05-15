namespace DirectMessages.Models.DTO
{
    public class CreateDmDto
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public string Message { get; set; }
    }
}
