using System.Collections.Generic;
using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.Domain.RepositoryContracts
{
    public interface IChatMessageRepository
    {
        bool Create(ChatMessage message);
        IEnumerable<ChatMessage> GetAll(int chatId);
    }
}
