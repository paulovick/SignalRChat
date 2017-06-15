using System;
using MySql.Data.MySqlClient;
using SignalRChat.Configuration.DataAccess.MariaDB.Contracts;

namespace SignalRChat.Configuration.DataAccess.MariaDB.Implementations
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private MySqlConnection Connection;

        public void InitRepository(string connectionString)
        {
            this.Connection = new MySqlConnection(connectionString);
        }

        public string GetValue(string key)
        {
            try
            {
                this.Connection.Open();

                var query = $"SELECT Value FROM ValueSettings WHERE `Key` = '{key}';";
                var command = new MySqlCommand(query, this.Connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var result = reader["Value"].ToString();
                        return result;
                    }
                }

                throw new Exception("Key not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't retrieve value from key: " + key, ex);
            }
            finally
            {
                this.Connection.Close();
            }
        }
    }
}
