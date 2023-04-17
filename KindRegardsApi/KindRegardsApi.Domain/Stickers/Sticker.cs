namespace KindRegardsApi.Domain.Stickers
{
    public class Sticker
    {
        public long Id {get; set;} = 0L;
        public string Image {get; set;} = "";

        // Default constructor
        public Sticker(){}

        public Sticker(long id, string image)
        {
            this.Id = id;
            this.Image = image;
        }
    }
}
