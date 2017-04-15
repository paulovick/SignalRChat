using System.Collections.Generic;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Dtos;

namespace SignalRChat.Site.ServiceLibrary.Services.Contracts
{
    public interface IUserService
    {
        User GetById(int senderId);
        User GetByUsername(string username);
        IEnumerable<User> GetAll();
        User Create(UserCreationDto userDto);
    }
}
