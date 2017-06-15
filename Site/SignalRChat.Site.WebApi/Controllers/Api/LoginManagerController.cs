using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;
using SignalRChat.Site.WebApi.Configuration;
using SignalRChat.Site.WebApi.Custom.Filters.Authorization;
using SignalRChat.Site.WebApi.Mappers.Contracts;
using SignalRChat.Site.WebApi.Models.Requests;
using SignalRChat.Site.WebApi.Models.Responses;
using SignalRChat.Utilities.Criptography;

namespace SignalRChat.Site.WebApi.Controllers.Api
{
    [RoutePrefix("api/loginManager")]
    public class LoginManagerController : Controller
    {
        private readonly IStringCipher iStringCipher;
        private readonly IUserService iUserService;
        private readonly IUserRequestMapper iUserRequestMapper;
        private readonly ISiteConfiguration iSiteConfiguration;

        public LoginManagerController(IStringCipher iStringCipher,
                                        IUserService iUserService,
                                        IUserRequestMapper iUserRequestMapper,
                                        ISiteConfiguration iSiteConfiguration)
        {
            this.iStringCipher = Guard.ArgumentNotNullAndReturn(iStringCipher, "iStringCipher");
            this.iUserService = Guard.ArgumentNotNullAndReturn(iUserService, "iUserService");
            this.iUserRequestMapper = Guard.ArgumentNotNullAndReturn(iUserRequestMapper, "iUserRequestMapper");
            this.iSiteConfiguration = Guard.ArgumentNotNullAndReturn(iSiteConfiguration, "iSiteConfiguration");
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(UserLoginRequest request)
        {
            var user = this.iUserService.GetByUsername(request.Username);
            if (user.Password != request.Password)
            {
                return new HttpUnauthorizedResult();
            }

            var expirationDays = 14;
            var response = this.GetResponse(user, expirationDays);
            return Json(response);
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(UserRegisterRequest request)
        {
            var userDto = this.iUserRequestMapper.Convert(request);
            var user = this.iUserService.Create(userDto);
            var expirationDays = 14;
            var response = this.GetResponse(user, expirationDays);
            return Json(response);
        }

        private UserLoginResponse GetResponse(User user, int expirationDays)
        {
            var token = this.GenerateToken(user.Id, expirationDays);
            var result = new UserLoginResponse
            {
                Token = token,
                TokenExpiration = expirationDays,
                User = user,
                UriToRedirect = this.iSiteConfiguration.SiteBaseUrl
            };
            return result;
        }

        private string GenerateToken(int userId, int expirationDays)
        {
            var token = new AuthorizationToken
            {
                UserId = userId,
                ExpirationDate = DateTime.Now.AddDays(expirationDays)
            };
            var tokenJson = JsonConvert.SerializeObject(token);
            var result = this.iStringCipher.Encrypt(tokenJson);
            return result;
        }
    }
}
