using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        User GetById(int id);
    }
}
