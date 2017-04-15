using System.Web.Mvc;
using SignalRChat.Site.WebApi.Custom.Filters.Authorization;

namespace SignalRChat.Site.WebApi.Controllers
{
    [SignalRChatAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}