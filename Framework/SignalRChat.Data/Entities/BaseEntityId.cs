namespace SignalRChat.Data.Entities
{
    public class BaseEntityId<T> : BaseEntity
    {
        public T Id { get; set; }
    }
}
