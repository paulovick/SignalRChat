using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.ServiceLocation;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;

namespace SignalRChat.Site.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService iChatService;

        public ChatHub() : this(ServiceLocator.Current.GetInstance<IChatService>())
        {
        }

        public ChatHub(IChatService iChatService)
        {
            this.iChatService = Guard.ArgumentNotNullAndReturn(iChatService, "iChatService");
        }

        public Task InitGroup(int chatId)
        {
            return Groups.Add(Context.ConnectionId, chatId.ToString());
        }

        public void SendMessage(int chatId, int senderId, string message)
        {
            var chatMessage = new ChatMessage
            {
                ChatId = chatId,
                SentDate = DateTime.Now,
                SenderUserId = senderId,
                Message = message,
                SeenDate = DateTime.Now
            };
            Clients.OthersInGroup(chatId.ToString()).addChatMessage(chatMessage);
            this.iChatService.AddMessage(chatMessage);
        }
    }
}