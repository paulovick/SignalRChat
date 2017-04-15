using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.WebApi.Custom.Filters.Authorization;
using SignalRChat.Site.WebApi.Models.Requests;
using SignalRChat.Site.WebApi.Models.Responses;
using SignalRChat.Utilities.Criptography;

namespace SignalRChat.Site.WebApi.Controllers.Api
{
    [RoutePrefix("api/loginManager")]
    public class LoginApiController : Controller
    {
        private readonly IStringCipher iStringCipher;

        public LoginApiController(IStringCipher iStringCipher)
        {
            this.iStringCipher = Guard.ArgumentNotNullAndReturn(iStringCipher, "iStringCipher");
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(UserLoginRequest request)
        {
            var userId = 1;
            var expirationDays = 14;
            var token = new AuthorizationToken
            {
                UserId = userId,
                ExpirationDate = DateTime.Now.AddDays(expirationDays)
            };
            var tokenJson = JsonConvert.SerializeObject(token);
            var encryptedToken = this.iStringCipher.Encrypt(tokenJson);
            var loginResponse = new UserLoginResponse
            {
                Token = encryptedToken,
                TokenExpiration = expirationDays,
                User = new User
                {
                    Id = userId,
                    Username = request.Username
                },
                UriToRedirect = "http://localhost:50276/"
            };
            var isAuthenticated = true;
            if (isAuthenticated)
            {
                return Json(loginResponse);
            }
            return null;
        }
    }
}
