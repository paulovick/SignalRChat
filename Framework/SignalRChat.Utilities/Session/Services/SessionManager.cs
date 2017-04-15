using System;
using System.Web;
using Newtonsoft.Json;
using SignalRChat.Utilities.Session.Models;

namespace SignalRChat.Utilities.Session.Services
{
    public class SessionManager : ISessionManager
    {
        public SessionUser GetSessionUserInfo(HttpContextBase context)
        {
            var loggedUserCookie = context.Request.Cookies.Get("loggedUser");
            if (loggedUserCookie == null)
            {
                throw new Exception("Unexisting loggedUser cookie");
            }
            var scapedUserCookie = Uri.UnescapeDataString(loggedUserCookie.Value);
            var result = JsonConvert.DeserializeObject<SessionUser>(scapedUserCookie);
            return result;
        }
    }
}
