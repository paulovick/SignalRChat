using System;
using SignalRChat.Utilities.Criptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using SignalRChat.ErrorControl.Utilities;

namespace SignalRChat.Site.WebApi.Custom.Filters.Authorization
{
    public class SignalRChatAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IStringCipher iStringCipher;

        public SignalRChatAuthorizeAttribute() : this (
            ServiceLocator.Current.GetInstance<IStringCipher>())
        { }

        public SignalRChatAuthorizeAttribute(IStringCipher iStringCipher)
        {
            this.iStringCipher = Guard.ArgumentNotNullAndReturn(iStringCipher, "iStringCipher");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var tokenCookie = httpContext.Request.Cookies.Get("authorizationToken");
            if (tokenCookie == null)
            {
                return false;
            }

            var token = this.ParseToken(tokenCookie.Value);
            if (token.ExpirationDate.CompareTo(DateTime.Now) < 0)
            {
                return false;
            }

            return true;
        }

        private AuthorizationToken ParseToken(string token)
        {
            var decryptedToken = this.iStringCipher.Decrypt(token);
            var result = JsonConvert.DeserializeObject<AuthorizationToken>(decryptedToken);
            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var routeValues = new RouteValueDictionary(new {
                controller = "login",
                action = "index"
            });
            filterContext.Result = new RedirectToRouteResult(routeValues);
        }
    }
}