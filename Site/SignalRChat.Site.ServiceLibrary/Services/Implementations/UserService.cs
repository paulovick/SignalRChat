using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.Domain.RepositoryContracts;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;

namespace SignalRChat.Site.ServiceLibrary.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;

        public UserService(IUserRepository iUserRepository)
        {
            this.iUserRepository = Guard.ArgumentNotNullAndReturn(iUserRepository, "iUserRepository");
        }

        public User GetById(int senderId)
        {
            var result = this.iUserRepository.GetById(senderId);
            return result;
        }
    }
}
