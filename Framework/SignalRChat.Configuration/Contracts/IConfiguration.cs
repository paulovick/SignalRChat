namespace SignalRChat.Configuration.Contracts
{
    public interface IConfiguration
    {
        T GetValue<T>(string key);
    }
}
