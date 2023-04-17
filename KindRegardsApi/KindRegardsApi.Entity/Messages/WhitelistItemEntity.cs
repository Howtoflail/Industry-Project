namespace KindRegardsApi.Entity.Messages
{
    public class WhitelistItemEntity
    {
        public long Id { get; set; } = 0;
        public string Text { get; set; } = "";

        // Default constructor
        public WhitelistItemEntity() { }

        public WhitelistItemEntity(long id,string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}
