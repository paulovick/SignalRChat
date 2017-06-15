using Autofac;
using SignalRChat.Site.DataAccess.MariaDB.Configuration;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Implementations;
using SignalRChat.Site.DataAccess.MariaDB.RepositoryImplementations;
using SignalRChat.Site.Domain.RepositoryContracts;

namespace SignalRChat.Site.DataAccess.MariaDB.IoC
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterConfigurations(builder);
            this.RegisterRepositories(builder);
            this.RegisterMappers(builder);
        }

        private void RegisterConfigurations(ContainerBuilder builder)
        {
            builder.RegisterType<SiteInfrastructureConfiguration>().As<ISiteInfrastructureConfiguration>();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ChatRepository>().As<IChatRepository>();
            builder.RegisterType<ChatMessageRepository>().As<IChatMessageRepository>();
        }

        private void RegisterMappers(ContainerBuilder builder)
        {
            builder.RegisterType<ChatMapper>().As<IChatMapper>();
            builder.RegisterType<ChatMessageMapper>().As<IChatMessageMapper>();
            builder.RegisterType<UserMapper>().As<IUserMapper>();
        }
    }
}
