namespace KindRegardsApi.Presentation.DTO.Stickers
{
    public class UpdateStickerDTO
    {
        public long Id {get; set;} = 0L;
        public string Image {get; set;} = "";

        // Default constructor
        public UpdateStickerDTO(){}

        public UpdateStickerDTO(long id, string image)
        {
            this.Id = id;
            this.Image = image;
        }
    }
}
