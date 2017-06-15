using System;
using MySql.Data.MySqlClient;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;
using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.DataAccess.MariaDB.Mappers.Implementations
{
    public class ChatMessageMapper : IChatMessageMapper
    {
        public ChatMessage Convert(MySqlDataReader reader)
        {
            var result = new ChatMessage
            {
                ChatId = Int32.Parse(reader["ChatId"].ToString()),
                SentDate = DateTime.Parse(reader["SentDate"].ToString()),
                SenderUserId = Int32.Parse(reader["SenderUserId"].ToString()),
                Message = reader["Message"].ToString(),
                SeenDate = DateTime.Parse(reader["SeenDate"].ToString())
            };

            return result;
        }
    }
}
