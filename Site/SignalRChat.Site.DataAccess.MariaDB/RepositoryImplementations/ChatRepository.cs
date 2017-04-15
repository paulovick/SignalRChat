using System;
using System.Collections.Generic;
using System.Linq;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.Domain.FilterDtos;
using SignalRChat.Site.Domain.RepositoryContracts;

namespace SignalRChat.Site.DataAccess.MariaDB.RepositoryImplementations
{
    public class ChatRepository : ChatRepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(IChatMapper iChatMapper)
            : base(iChatMapper)
        {
        }

        public IEnumerable<Chat> GetByFilter(ChatFilterDto filter)
        {
            var result = new List<Chat>();
            try
            {
                this.OpenConnection();

                result = this.ExecuteSearch(filter);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve chats by filter. ", ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public int Create(int firstUserId, int secondUserId)
        {
            try
            {
                this.OpenConnection();

                var dateFormat = "yyyy-MM-dd HH:mm:ss";
                var query = $"INSERT INTO Chat(FirstUserId,SecondUserId,CreatedAt,LastModifiedAt) VALUES({firstUserId},{secondUserId},'{DateTime.Now.ToString(dateFormat)}','{DateTime.Now.ToString(dateFormat)}')";
                this.ExecuteNonQuery(query);

                var filter = new ChatFilterDto
                {
                    FirstUserId = firstUserId,
                    SecondUserId = secondUserId
                };
                var chat = this.ExecuteSearch(filter).FirstOrDefault();
                return chat.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"The chat {firstUserId} -> {secondUserId} was not correctly created.", ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        private List<Chat> ExecuteSearch(ChatFilterDto filter)
        {
            var query = this.GetQuery(filter);
            var result = this.ExecuteMultipleQuery(query).ToList();
            return result;
        }

        private string GetQuery(ChatFilterDto filter)
        {
            var result = "SELECT * FROM Chat";
            if (!filter.HasFilters)
            {
                return result + ";";
            }

            result += " WHERE ";
            var actualConditions = 0;
            if (filter.ChatId.HasValue)
            {
                if (actualConditions > 0)
                {
                    result += " AND ";
                }
                result += $"ChatId = '{filter.ChatId.Value}'";
                actualConditions++;
            }
            if (filter.FirstUserId.HasValue)
            {
                if (actualConditions > 0)
                {
                    result += " AND ";
                }
                result += $"FirstUserId = '{filter.FirstUserId.Value}'";
                actualConditions++;
            }
            if (filter.SecondUserId.HasValue)
            {
                if (actualConditions > 0)
                {
                    result += " AND ";
                }
                result += $"SecondUserId = '{filter.SecondUserId.Value}'";
                actualConditions++;
            }

            return result + ";";
        }
    }
}
