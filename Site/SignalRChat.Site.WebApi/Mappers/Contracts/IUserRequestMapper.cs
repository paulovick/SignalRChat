using SignalRChat.Site.ServiceLibrary.Dtos;
using SignalRChat.Site.WebApi.Models.Requests;

namespace SignalRChat.Site.WebApi.Mappers.Contracts
{
    public interface IUserRequestMapper
    {
        UserCreationDto Convert(UserRegisterRequest source);
    }
}
