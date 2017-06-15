using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRChat.Site.WebApi.Startup))]
namespace SignalRChat.Site.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}