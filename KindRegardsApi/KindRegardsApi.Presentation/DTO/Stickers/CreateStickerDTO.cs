namespace KindRegardsApi.Presentation.DTO.Stickers
{
    public class CreateStickerDTO
    {
        public string Image {get; set;} = "";

        // Default constructor
        public CreateStickerDTO(){}

        public CreateStickerDTO(string image)
        {
            this.Image = image;
        }
    }
}
