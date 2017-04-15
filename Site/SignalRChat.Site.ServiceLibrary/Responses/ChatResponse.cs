using System.Collections.Generic;
using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.ServiceLibrary.Responses
{
    public class ChatResponse
    {
        public Chat Chat { get; set; }
        public IEnumerable<ChatMessage> Messages { get; set; }
    }
}