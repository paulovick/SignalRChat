using System;
using System.Collections.Generic;
using System.Linq;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.Domain.RepositoryContracts;
using SignalRChat.Site.ServiceLibrary.Dtos;
using SignalRChat.Site.ServiceLibrary.Mappers.Contracts;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;

namespace SignalRChat.Site.ServiceLibrary.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;
        private readonly IUserMapper iUserMapper;

        public UserService(IUserRepository iUserRepository, IUserMapper iUserMapper)
        {
            this.iUserRepository = Guard.ArgumentNotNullAndReturn(iUserRepository, "iUserRepository");
            this.iUserMapper = Guard.ArgumentNotNullAndReturn(iUserMapper, "iUserMapper");
        }

        public User GetById(int senderId)
        {
            var result = this.iUserRepository.GetById(senderId);
            return result;
        }

        public User GetByUsername(string username)
        {
            var users = this.iUserRepository.GetAll();
            var result = users.FirstOrDefault(x => x.Username == username);
            return result;
        }

        public IEnumerable<User> GetAll()
        {
            var result = this.iUserRepository.GetAll();
            return result;
        }

        public User Create(UserCreationDto userDto)
        {
            var user = this.iUserMapper.Convert(userDto);
            this.iUserRepository.Insert(user);
            var result = this.iUserRepository.GetAll().FirstOrDefault(x => x.Username == user.Username);
            return result;
        }
    }
}
