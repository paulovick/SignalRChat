using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Microsoft.Practices.ServiceLocation;
using SignalRChat.Site.WebApi.Mappers.Contracts;
using SignalRChat.Site.WebApi.Mappers.Implementations;
using AutofacDependencyResolver = Autofac.Integration.Mvc.AutofacDependencyResolver;

namespace SignalRChat.Site.WebApi.App_Start
{
    public class IoCConfig
    {
        public static IContainer RegisterDependences()
        {
            var builder = new ContainerBuilder();

            RegisterControllers(builder);
            RegisterHubs(builder);

            var modules = new List<Autofac.Module>
            {
                new SignalRChat.Utilities.IoC.IoCModule(),
                new SignalRChat.Site.ServiceLibrary.IoC.IoCModule(),
                new SignalRChat.Site.DataAccess.MariaDB.IoC.IoCModule()
            };

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            RegisterSelfTypes(builder);

            var container = builder.Build();
            SetDependencyResolver(container);
            return container;
        }

        private static void RegisterSelfTypes(ContainerBuilder builder)
        {
            builder.RegisterType<UserRequestMapper>().As<IUserRequestMapper>();
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }

        private static void RegisterHubs(ContainerBuilder builder)
        {
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
        }

        private static void SetDependencyResolver(IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }
    }
}