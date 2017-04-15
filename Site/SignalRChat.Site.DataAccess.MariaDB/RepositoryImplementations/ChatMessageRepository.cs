using System;
using System.Collections.Generic;
using System.Linq;
using SignalRChat.Site.DataAccess.MariaDB.Mappers.Contracts;
using SignalRChat.Site.Domain.Entities;
using SignalRChat.Site.Domain.RepositoryContracts;

namespace SignalRChat.Site.DataAccess.MariaDB.RepositoryImplementations
{
    public class ChatMessageRepository : ChatRepositoryBase<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(IChatMessageMapper iChatMessageMapper)
            : base(iChatMessageMapper)
        {
        }

        public bool Create(ChatMessage message)
        {
            var result = false;
            try
            {
                this.OpenConnection();

                var dateFormat = "yyyy-MM-dd HH:mm:ss";
                string query = "INSERT INTO ChatMessage(ChatId, SentDate, SenderUserId, Message, SeenDate) " +
                               $"VALUES ({message.ChatId}, '{message.SentDate.ToString(dateFormat)}', '{message.SenderUserId}', '{message.Message}', '{message.SentDate.ToString(dateFormat)}');";
                this.ExecuteNonQuery(query);

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public IEnumerable<ChatMessage> GetAll(int chatId)
        {
            var result = new List<ChatMessage>();
            try
            {
                this.OpenConnection();

                string query = $"SELECT * FROM ChatMessage WHERE ChatId='{chatId}' ORDER BY SentDate ASC;";
                result = this.ExecuteMultipleQuery(query).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't get messages for chat {chatId}.", ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }
    }
}
