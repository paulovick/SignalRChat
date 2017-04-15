using Autofac;
using SignalRChat.Utilities.Criptography;

namespace SignalRChat.Utilities.IoC
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StringCipher>().As<IStringCipher>();
        }
    }
}
