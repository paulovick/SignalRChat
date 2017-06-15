using SignalRChat.Configuration.Contracts;
using SignalRChat.ErrorControl.Utilities;

namespace SignalRChat.Site.WebApi.Configuration
{
    public class SiteConfiguration : ISiteConfiguration
    {
        private readonly IConfiguration iConfiguration;

        public SiteConfiguration(IConfiguration iConfiguration)
        {
            this.iConfiguration = Guard.ArgumentNotNullAndReturn(iConfiguration, "iConfiguration");
        }

        public string SiteBaseUrl
        {
            get
            {
                var result = this.iConfiguration.GetValue<string>("SiteBaseUrl");
                return result;
            }
        }
    }
}