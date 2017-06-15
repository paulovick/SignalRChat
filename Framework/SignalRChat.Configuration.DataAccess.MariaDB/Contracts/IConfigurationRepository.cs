using System.Globalization;

namespace SignalRChat.Configuration.DataAccess.MariaDB.Contracts
{
    public interface IConfigurationRepository
    {
        void InitRepository(string connectionString);
        string GetValue(string key);
    }
}
