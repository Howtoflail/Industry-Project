namespace KindRegardsApi.Presentation.DTO.Messages
{
    public class WhitelistItemDTO
    {
        public long Id { get; set; } = 0;
        public string Text { get; set; } = "";

        // Default constructor
        public WhitelistItemDTO() { }
        public WhitelistItemDTO(long id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}
