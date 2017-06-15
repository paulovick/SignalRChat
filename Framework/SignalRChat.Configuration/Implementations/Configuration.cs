using System.ComponentModel;
using SignalRChat.Configuration.Contracts;
using SignalRChat.Configuration.DataAccess.MariaDB.Contracts;
using SignalRChat.ErrorControl.Utilities;

namespace SignalRChat.Configuration.Implementations
{
    public class Configuration : IConfiguration
    {
        private readonly IConfigurationRepository iConfigurationRepository;

        public Configuration(IConfigurationRepository iConfigurationRepository)
        {
            this.iConfigurationRepository = Guard.ArgumentNotNullAndReturn(iConfigurationRepository, "iConfigurationRepository");

            var resourceKey = "ConfigurationPro";
#if DEBUG
            resourceKey = "ConfigurationDev";
#endif
            var connectionString = Properties.Resources.ResourceManager.GetString(resourceKey);
            this.iConfigurationRepository.InitRepository(connectionString);
        }

        public T GetValue<T>(string key)
        {
            var valueString = this.iConfigurationRepository.GetValue(key);
            var result = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(valueString);
            return result;
        }
    }
}
