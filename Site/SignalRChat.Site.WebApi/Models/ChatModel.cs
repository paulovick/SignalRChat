using System.Collections.Generic;
using System.Web.UI;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.WebApi.Models.Responses;

namespace SignalRChat.Site.WebApi.Models
{
    public class ChatModel
    {
        public Chat Chat { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public IEnumerable<ChatMessageResponse> Messages { get; set; }
    }
}