using Autofac;
using SignalRChat.Configuration.Contracts;

namespace SignalRChat.Configuration.IoC
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Implementations.Configuration>().As<IConfiguration>();
        }
    }
}
