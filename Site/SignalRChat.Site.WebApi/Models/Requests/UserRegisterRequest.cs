namespace SignalRChat.Site.WebApi.Models.Requests
{
    public class UserRegisterRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}