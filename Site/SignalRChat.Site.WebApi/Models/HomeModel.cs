using System.Collections.Generic;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Utilities.Session.Models;

namespace SignalRChat.Site.WebApi.Models
{
    public class HomeModel
    {
        public SessionUser SessionUser { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}