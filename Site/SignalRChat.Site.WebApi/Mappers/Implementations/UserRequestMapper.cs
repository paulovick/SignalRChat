using SignalRChat.Site.ServiceLibrary.Dtos;
using SignalRChat.Site.WebApi.Mappers.Contracts;
using SignalRChat.Site.WebApi.Models.Requests;

namespace SignalRChat.Site.WebApi.Mappers.Implementations
{
    public class UserRequestMapper : IUserRequestMapper
    {
        public UserCreationDto Convert(UserRegisterRequest source)
        {
            var result = new UserCreationDto
            {
                Email = source.Email,
                Username = source.Username,
                Password = source.Password
            };
            return result;
        }
    }
}