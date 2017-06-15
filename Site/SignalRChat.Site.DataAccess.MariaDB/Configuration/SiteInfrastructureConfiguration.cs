using SignalRChat.Configuration.Contracts;
using SignalRChat.ErrorControl.Utilities;

namespace SignalRChat.Site.DataAccess.MariaDB.Configuration
{
    public class SiteInfrastructureConfiguration : ISiteInfrastructureConfiguration
    {
        private readonly IConfiguration iConfiguration;

        public SiteInfrastructureConfiguration(IConfiguration iConfiguration)
        {
            this.iConfiguration = Guard.ArgumentNotNullAndReturn(iConfiguration, "iConfiguration");
        }

        public string ChatConnectionString
        {
            get
            {
                var result = this.iConfiguration.GetValue<string>("ChatDBConnectionString");
                return result;
            }
        }
    }
}
