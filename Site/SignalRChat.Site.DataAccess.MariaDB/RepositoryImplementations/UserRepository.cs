using System;
using System.Collections.Generic;
using System.Linq;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.Domain.RepositoryContracts;

namespace SignalRChat.Site.DataAccess.MariaDB.RepositoryImplementations
{
    public class UserRepository : ChatRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IUserMapper iUserMapper) : base(iUserMapper)
        {
        }

        public User GetById(int id)
        {
            var result = default(User);
            try
            {
                this.OpenConnection();

                var query = $"SELECT * FROM User WHERE Id = {id};";
                result = this.ExecuteSingleQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve user with id {id}", ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> result = new List<User>();
            try
            {
                this.OpenConnection();

                var query = "SELECT * FROM User;";
                result = this.ExecuteMultipleQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't retrieve all users.", ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public void Insert(User user)
        {
            try
            {
                this.OpenConnection();

                var query = "INSERT INTO User(Email, Username, Password, CreatedAt, LastModifiedAt) VALUES (" +
                            $"{user.Email}, {user.Username}, {user.Password}, {user.CreatedAd}, {user.LastModifiedAt});";
                this.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't create user.", ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
