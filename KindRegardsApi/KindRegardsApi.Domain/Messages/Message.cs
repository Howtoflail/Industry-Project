namespace KindRegardsApi.Domain.Messages
{
    public class Message
    {
        public long Id { get; set; }
        // Prevent replying to yourself by using the DeviceId
        public string DeviceId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        // Track if the message has been read or not
        public bool Read { get; set; } = false;
        //Track if the message has been thanked or not
        public bool Thanked { get; set; }= false;
        public Gift? Gift { get; set; }

        // Default constructor
        public Message() { }

        public Message(long id,string deviceId,string text, DateTime date)
        {
            this.Id = id;
            this.DeviceId = deviceId;
            this.Text = text;
            this.Date = date;
        }
    }
}
