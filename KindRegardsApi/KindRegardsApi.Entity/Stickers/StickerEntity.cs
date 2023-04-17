namespace KindRegardsApi.Entity.Stickers
{
    public class StickerEntity
    {
        public long Id {get; set;} = 0;
        public string Image {get; set;} = "";

        // Default constructor
        public StickerEntity(){}

        public StickerEntity(long id, string image)
        {
            this.Id = id;
            this.Image = image;
        }
    }
}
