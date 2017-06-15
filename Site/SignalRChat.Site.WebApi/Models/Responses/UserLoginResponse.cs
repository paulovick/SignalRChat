using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.WebApi.Models.Responses
{
    public class UserLoginResponse
    {
        public string Token { get; set; }
        public int TokenExpiration { get; set; }
        public User User { get; set; }
        public string UriToRedirect { get; set; }
    }
}