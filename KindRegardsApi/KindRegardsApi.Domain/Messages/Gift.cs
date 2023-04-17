namespace KindRegardsApi.Domain.Messages
{
    public class Gift
    {
        public long Id { get; set; }
        public long StickerId { get; set; }
        public Color Color { get; set; }

        // Default constructor
        public Gift() { }

        public Gift(long id, long stickerId,Color color)
        {
            this.Id = id;
            this.StickerId = stickerId;
            this.Color = color;
        }

    }
}
