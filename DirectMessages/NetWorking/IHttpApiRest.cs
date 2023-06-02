namespace DirectMessages.NetWorking
{
    public interface IHttpApiRest
    {
        Task<bool> SubscribeToChannel(List<Guid> id, Guid channelid);
        Task<string> GetUserNameById(Guid id);
    }
}
