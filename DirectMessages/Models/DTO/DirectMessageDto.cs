namespace DirectMessages.Models.DTO
{
    public class DirectMessageDto
    {
        public Guid ChannelId { get; set; }
        public Guid SenderId { get; set; }
        public string Message { get; set; }
    }
}
