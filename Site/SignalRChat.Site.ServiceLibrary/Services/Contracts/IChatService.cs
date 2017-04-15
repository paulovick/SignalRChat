using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Responses;

namespace SignalRChat.Site.ServiceLibrary.Services.Contracts
{
    public interface IChatService
    {
        ChatResponse GetOrCreateChat(int senderId, int receiverId);
        void AddMessage(ChatMessage chatMessage);
    }
}
