using System;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Dtos;
using SignalRChat.Site.ServiceLibrary.Mappers.Contracts;

namespace SignalRChat.Site.ServiceLibrary.Mappers.Implementations
{
    public class UserMapper : IUserMapper
    {
        public User Convert(UserCreationDto source)
        {
            var result = new User
            {
                Email = source.Email,
                Username = source.Username,
                Password = source.Password,
                CreatedAd = DateTime.Now,
                LastModifiedAt = DateTime.Now
            };
            return result;
        }
    }
}
