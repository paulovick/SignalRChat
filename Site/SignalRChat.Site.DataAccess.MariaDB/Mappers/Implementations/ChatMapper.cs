using System;
using MySql.Data.MySqlClient;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;
using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.DataAccess.MariaDB.Mappers.Implementations
{
    public class ChatMapper : IChatMapper
    {
        public Chat Convert(MySqlDataReader reader)
        {
            var result = new Chat
            {
                Id = Int32.Parse(reader["Id"].ToString()),
                FirstUserId = Int32.Parse(reader["FirstUserId"].ToString()),
                SecondUserId = Int32.Parse(reader["SecondUserId"].ToString()),
                CreatedAd = DateTime.Parse(reader["CreatedAt"].ToString()),
                LastModifiedAt = DateTime.Parse(reader["LastModifiedAt"].ToString()),
            };

            return result;
        }
    }
}
