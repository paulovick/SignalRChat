namespace SignalRChat.Site.Domain.FilterDtos
{
    public class ChatFilterDto
    {
        public int? ChatId { get; set; }
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }

        public bool HasFilters => ChatId.HasValue || FirstUserId.HasValue || SecondUserId.HasValue;
    }
}
