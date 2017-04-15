using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;
using SignalRChat.Site.WebApi.Custom.Filters.Authorization;
using SignalRChat.Site.WebApi.Models;
using SignalRChat.Site.WebApi.Models.Responses;

namespace SignalRChat.Site.WebApi.Controllers
{
    [SignalRChatAuthorize]
    [RoutePrefix("chat")]
    public class ChatController : Controller
    {
        private readonly IChatService iChatService;
        private readonly IUserService iUserService;

        public ChatController(IChatService iChatService, IUserService iUserService)
        {
            this.iChatService = Guard.ArgumentNotNullAndReturn(iChatService, "iChatService");
            this.iUserService = Guard.ArgumentNotNullAndReturn(iUserService, "iUserService");
        }

        [Route("{receiverId}")]
        public ActionResult Index(int receiverId)
        {
            var senderId = this.GetUserId();
            var model = this.GetModel(senderId, receiverId);
            return View(model);
        }

        private int GetUserId()
        {
            var loggedUserCookie = HttpContext.Request.Cookies.Get("loggedUser");
            if (loggedUserCookie == null)
            {
                throw new Exception("Unexisting loggedUser cookie");
            }
            var decodedUserCookie = Uri.UnescapeDataString(loggedUserCookie.Value);
            var loggedUser = JsonConvert.DeserializeObject<User>(decodedUserCookie);
            return loggedUser.Id;
        }

        private ChatModel GetModel(int senderId, int receiverId)
        {
            var chat = this.iChatService.GetOrCreateChat(senderId, receiverId);
            var sender = this.iUserService.GetById(senderId);
            var receiver = this.iUserService.GetById(receiverId);

            var result = new ChatModel
            {
                Chat = chat.Chat,
                Sender = sender,
                Receiver = receiver,
                Messages = chat.Messages.Select(x => this.ConvertMessage(x, sender, receiver))
            };
            return result;
        }

        private ChatMessageResponse ConvertMessage(ChatMessage chatMessage, User sender, User receiver)
        {
            var senderName = sender.Id == chatMessage.SenderUserId ? sender.Username : receiver.Username;
            var result = new ChatMessageResponse
            {
                ChatMessage = chatMessage,
                SenderName = senderName
            };
            return result;
        }
    }
}