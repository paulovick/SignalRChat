
using System.Web;
using SignalRChat.Utilities.Session.Models;

namespace SignalRChat.Utilities.Session.Services
{
    public interface ISessionManager
    {
        SessionUser GetSessionUserInfo(HttpContextBase context);
    }
}
