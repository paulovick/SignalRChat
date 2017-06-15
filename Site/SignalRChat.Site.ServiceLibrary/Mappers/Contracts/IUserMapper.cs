using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Dtos;

namespace SignalRChat.Site.ServiceLibrary.Mappers.Contracts
{
    public interface IUserMapper
    {
        User Convert(UserCreationDto source);
    }
}
