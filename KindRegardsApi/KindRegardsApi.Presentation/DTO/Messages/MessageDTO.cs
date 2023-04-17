namespace KindRegardsApi.Presentation.DTO.Messages
{
    public class MessageDTO
    {
        public long Id { get; set; } = 0;
        public string DeviceId { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime Date { get; set; }

        // Default constructor
        public MessageDTO() { }
        public MessageDTO(long id, string deviceId, string text,DateTime date)
        {
            this.Id = id;
            this.DeviceId = deviceId;
            this.Text = text;
            this.Date = date;
        }
    }
}
