using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;
using SignalRChat.Site.WebApi.Custom.Filters.Authorization;
using SignalRChat.Site.WebApi.Models;
using SignalRChat.Site.WebApi.Models.Responses;
using SignalRChat.Utilities.Session.Services;

namespace SignalRChat.Site.WebApi.Controllers
{
    [SignalRChatAuthorize]
    [RoutePrefix("chat")]
    public class ChatController : Controller
    {
        private readonly IChatService iChatService;
        private readonly IUserService iUserService;
        private readonly ISessionManager iSessionManager;

        public ChatController(IChatService iChatService,
                                IUserService iUserService,
                                ISessionManager iSessionManager)
        {
            this.iChatService = Guard.ArgumentNotNullAndReturn(iChatService, "iChatService");
            this.iUserService = Guard.ArgumentNotNullAndReturn(iUserService, "iUserService");
            this.iSessionManager = Guard.ArgumentNotNullAndReturn(iSessionManager, "iSessionManager");
        }

        [Route("{receiverId}")]
        public ActionResult Index(int receiverId)
        {
            var senderId = this.GetUserId();
            if (senderId == receiverId)
            {
                return View("NotFound");
            }

            var model = this.GetModel(senderId, receiverId);
            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        private int GetUserId()
        {
            var sessionUser = this.iSessionManager.GetSessionUserInfo(this.HttpContext);
            return sessionUser.Id;
        }

        private ChatModel GetModel(int senderId, int receiverId)
        {
            var result = default(ChatModel);
            try
            {
                var chat = this.iChatService.GetOrCreateChat(senderId, receiverId);
                var sender = this.iUserService.GetById(senderId);
                var receiver = this.iUserService.GetById(receiverId);

                result = new ChatModel
                {
                    Chat = chat.Chat,
                    Sender = sender,
                    Receiver = receiver,
                    Messages = chat.Messages.Select(x => this.ConvertMessage(x, sender, receiver))
                };
            }
            catch (Exception)
            {
                return null;
            }
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