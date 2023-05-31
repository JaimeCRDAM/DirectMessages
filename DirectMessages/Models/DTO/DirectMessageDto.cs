namespace DirectMessages.Models.DTO
{
    public class DirectMessageDto
    {
        public Guid Id { get; set; }
        public Guid ChannelId { get; set; }
        public User Sender { get; set; }
        public string Message { get; set; }
    }
}
