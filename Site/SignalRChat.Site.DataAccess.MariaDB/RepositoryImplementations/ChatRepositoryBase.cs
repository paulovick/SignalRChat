using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SignalRChat.ErrorControl.Utilities;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;

namespace SignalRChat.Site.DataAccess.MariaDB.RepositoryImplementations
{
    public class ChatRepositoryBase<T>
    {
        private readonly IChatMapperBase<T> iChatMapper;

        private readonly MySqlConnection Connection;

        public ChatRepositoryBase(IChatMapperBase<T> iChatMapper)
        {
            this.iChatMapper = Guard.ArgumentNotNullAndReturn(iChatMapper, "iChatMapper");

            var serverName = "signalr-chat-db.cuoz0vogfwqf.us-west-2.rds.amazonaws.com";
            var database = "Chat-Dev";
            var uid = "admin";
            var password = "7rNwwKQE";

            var connectionString = $"Server={serverName};Database={database};Uid={uid};Pwd={password}";
            this.Connection = new MySqlConnection(connectionString);
        }

        protected void OpenConnection()
        {
            this.Connection.Open();
        }

        protected void CloseConnection()
        {
            this.Connection.Close();
        }

        protected void ExecuteNonQuery(string query)
        {
            var command = new MySqlCommand(query, this.Connection);
            command.ExecuteNonQuery();
        }

        protected T ExecuteSingleQuery(string query)
        {
            var result = default(T);

            var command = new MySqlCommand(query, this.Connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = this.iChatMapper.Convert(reader);
                }
            }

            return result;
        }

        protected IEnumerable<T> ExecuteMultipleQuery(string query)
        {
            var result = new List<T>();

            var command = new MySqlCommand(query, this.Connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = this.iChatMapper.Convert(reader);
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
