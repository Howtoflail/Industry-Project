namespace KindRegardsApi.Presentation.DTO.Messages
{
    public class GiftDTO
    {
        public long Id { get; set; } = 0;
        public long StickerId { get; set; }

        // Default constructor
        public GiftDTO() { }
        public GiftDTO(long id, long stickerId)
        {
            this.Id = id;
            this.StickerId = stickerId;
        }
    }
}
