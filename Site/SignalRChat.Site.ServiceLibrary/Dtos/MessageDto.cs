namespace SignalRChat.Site.ServiceLibrary.Dtos
{
    public class MessageDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
