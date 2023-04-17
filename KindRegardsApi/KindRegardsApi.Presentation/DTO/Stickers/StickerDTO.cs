namespace KindRegardsApi.Presentation.DTO.Stickers
{
    public class StickerDTO
    {
        public long Id {get; set;} = 0;
        public string Image {get; set;} = "";
        public int Amount {get; set;} = 0;
        public bool Unlocked {get; set;} = false;

        // Default constructor
        public StickerDTO(){}
        public StickerDTO(long id, string image, int amount, bool unlocked)
        {
            this.Id = id;
            this.Image = image;
            this.Amount = amount;
            this.Unlocked = unlocked;
        }
    }
}
