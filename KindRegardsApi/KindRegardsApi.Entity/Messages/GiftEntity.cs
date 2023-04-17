namespace KindRegardsApi.Entity.Messages
{
    public class GiftEntity
    {
        public long Id { get; set; }
        public long StickerId { get; set; }

        // Default constructor
        public GiftEntity() { }

        public GiftEntity(long id, long stickerId)
        {
            this.Id = id;
            this.StickerId = stickerId;
        }
    }
}
