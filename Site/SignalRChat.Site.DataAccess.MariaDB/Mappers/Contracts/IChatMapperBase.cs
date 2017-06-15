using MySql.Data.MySqlClient;

namespace SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts
{
    public interface IChatMapperBase<out T>
    {
        T Convert(MySqlDataReader reader);
    }
}
