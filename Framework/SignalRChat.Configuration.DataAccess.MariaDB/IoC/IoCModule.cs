using Autofac;
using SignalRChat.Configuration.DataAccess.MariaDB.Contracts;
using SignalRChat.Configuration.DataAccess.MariaDB.Implementations;

namespace SignalRChat.Configuration.DataAccess.MariaDB.IoC
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>();
        }
    }
}
