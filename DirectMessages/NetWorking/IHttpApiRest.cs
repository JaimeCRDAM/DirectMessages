namespace DirectMessages.NetWorking
{
    public interface IHttpApiRest
    {
        Task<bool> SubscribeToChannel(List<Guid> id);
    }
}
