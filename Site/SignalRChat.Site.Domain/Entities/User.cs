using SignalRChat.Data.Entities;

namespace SignalRChat.Site.Domain.Entities
{
    public class User : BaseEntityId<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
