using SignalRChat.Data.Entities;

namespace SignalRChat.Site.Domain.Entities
{
    public class Chat : BaseEntityId<int>
    {
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
    }
}
