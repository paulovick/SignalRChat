using System.Collections.Generic;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.Domain.FilterDtos;

namespace SignalRChat.Site.Domain.RepositoryContracts
{
    public interface IChatRepository
    {
        IEnumerable<Chat> GetByFilter(ChatFilterDto filter);
        int Create(int firstUserId, int secondUserId);
    }
}
