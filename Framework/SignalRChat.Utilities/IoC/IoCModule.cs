using Autofac;
using SignalRChat.Utilities.Criptography;
using SignalRChat.Utilities.Session.Services;

namespace SignalRChat.Utilities.IoC
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StringCipher>().As<IStringCipher>();
            builder.RegisterType<SessionManager>().As<ISessionManager>();
        }
    }
}
