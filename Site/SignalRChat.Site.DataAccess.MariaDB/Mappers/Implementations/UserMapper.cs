using System;
using MySql.Data.MySqlClient;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;
using SignalRChat.Site.Domain.Entities;

namespace SignalRChat.Site.DataAccess.MariaDB.Mappers.Implementations
{
    public class UserMapper : IUserMapper
    {
        public User Convert(MySqlDataReader reader)
        {
            var result = new User
            {
                Id = Int32.Parse(reader["Id"].ToString()),
                Username = reader["Username"].ToString(),
                Password = reader["Password"].ToString(),
                Email = reader["Email"].ToString(),
                CreatedAd = DateTime.Parse(reader["CreatedAt"].ToString()),
                LastModifiedAt = DateTime.Parse(reader["LastModifiedAt"].ToString()),
            };

            return result;
        }
    }
}
