namespace KindRegardsApi.Domain.Messages
{
    public class WhitelistItem
    {
        public long Id { get; set; }
        public string Text { get; set; }
        // Default constructor
        public WhitelistItem() { }

        public WhitelistItem(long id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}
