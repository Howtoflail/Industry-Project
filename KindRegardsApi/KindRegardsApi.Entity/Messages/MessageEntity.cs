namespace KindRegardsApi.Entity.Messages
{
    public class MessageEntity
    {
        public long Id { get; set; } = 0;
        // Prevent replying to yourself by using the DeviceId
        public DeviceEntity? Device { get; set; } = null;
        public string Text { get; set; } = "";
        public DateTime Date { get; set; }
        public bool Read { get; set; } = false;
        public bool Thanked { get; set; } = false;
        public GiftEntity? Gift { get; set; } = null;

        // Default constructor
        public MessageEntity() { }

        public MessageEntity(long id, DeviceEntity device, string text, DateTime date)
        {
            this.Id = id;
            this.Device = device;
            this.Text = text;
            this.Date = date;
        }
    }
}
