using System;
using System.Collections.Generic;
using System.Linq;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.Domain.FilterDtos;
using SignalRChat.Site.Domain.RepositoryContracts;
using SignalRChat.Site.ServiceLibrary.Dtos;
using SignalRChat.Site.ServiceLibrary.Responses;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;

namespace SignalRChat.Site.ServiceLibrary.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository iChatRepository;
        private readonly IChatMessageRepository iChatMessageRepository;

        public ChatService(IChatRepository iChatRepository, IChatMessageRepository iChatMessageRepository)
        {
            this.iChatRepository = Guard.ArgumentNotNullAndReturn(iChatRepository, "iChatRepository"); ;
            this.iChatMessageRepository = Guard.ArgumentNotNullAndReturn(iChatMessageRepository, "iChatMessageRepository");
        }

        public ChatResponse GetOrCreateChat(int senderId, int receiverId)
        {
            var firstUserId = senderId < receiverId
                    ? senderId
                    : receiverId;
            var secondUserId = senderId < receiverId
                ? receiverId
                : senderId;

            var filter = new ChatFilterDto
            {
                FirstUserId = firstUserId,
                SecondUserId = secondUserId
            };
            var chat = this.iChatRepository.GetByFilter(filter).FirstOrDefault();
            if (chat == default(Chat))
            {
                var chatId = this.iChatRepository.Create(firstUserId, secondUserId);
                chat = new Chat
                {
                    Id = chatId,
                    FirstUserId = firstUserId,
                    SecondUserId = secondUserId
                };
            }
            
            var messages = this.GetMessages(chat.Id);

            var result = new ChatResponse
            {
                Chat = chat,
                Messages = messages
            };
            return result;
        }

        private IEnumerable<ChatMessage> GetMessages(int chatId)
        {
            var result = this.iChatMessageRepository.GetAll(chatId);
            return result;
        }

        public void AddMessage(ChatMessage chatMessage)
        {
            this.iChatMessageRepository.Create(chatMessage);
        }
    }
}
