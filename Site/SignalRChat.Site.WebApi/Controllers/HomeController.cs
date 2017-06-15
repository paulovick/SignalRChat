using System.Linq;
using System.Web.Mvc;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;
using SignalRChat.Site.WebApi.Configuration;
using SignalRChat.Site.WebApi.Custom.Filters.Authorization;
using SignalRChat.Site.WebApi.Models;
using SignalRChat.Utilities.Session.Services;

namespace SignalRChat.Site.WebApi.Controllers
{
    [SignalRChatAuthorize]
    public class HomeController : Controller
    {
        private readonly ISessionManager iSessionManager;
        private readonly IUserService iUserService;

        public HomeController(ISessionManager iSessionManager,
                                IUserService iUserService)
        {
            this.iSessionManager = Guard.ArgumentNotNullAndReturn(iSessionManager, "iSessionManager");
            this.iUserService = Guard.ArgumentNotNullAndReturn(iUserService, "iUserService");
        }

        public ActionResult Index()
        {
            var model = this.GetModel();
            return View(model);
        }

        private HomeModel GetModel()
        {
            var sessionUser = this.iSessionManager.GetSessionUserInfo(this.HttpContext);
            var users = this.iUserService.GetAll().Where(x => x.Id != sessionUser.Id);
            var result = new HomeModel
            {
                SessionUser = sessionUser,
                Users = users
            };
            return result;
        }
    }
}