using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.ServiceLibrary.Services.Contracts
{
    public interface IUserService
    {
        User GetById(int senderId);
        User GetByUsername(string username);
    }
}
