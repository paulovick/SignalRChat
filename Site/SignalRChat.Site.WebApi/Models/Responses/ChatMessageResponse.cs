using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.WebApi.Models.Responses
{
    public class ChatMessageResponse
    {
        public ChatMessage ChatMessage { get; set; }
        public string SenderName { get; set; }
    }
}