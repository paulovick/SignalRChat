using System;

namespace SignalRChat.Site.WebApi.Custom.Filters.Authorization
{
    public class AuthorizationToken
    {
        public int UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}