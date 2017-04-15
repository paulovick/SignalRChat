﻿using Autofac;
using SignalRChat.Site.ServiceLibrary.Services.Contracts;
using SignalRChat.Site.ServiceLibrary.Services.Implementations;

namespace SignalRChat.Site.ServiceLibrary.IoC
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ChatService>().As<IChatService>();
        }
    }
}
