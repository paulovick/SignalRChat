using System;

namespace SignalRChat.Site.Domain.Entities
{
    public class ChatMessage
    {
        public int ChatId { get; set; }
        public DateTime SentDate { get; set; }
        public int SenderUserId { get; set; }
        public string Message { get; set; }
        public DateTime? SeenDate { get; set; }
    }
}
