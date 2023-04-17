namespace KindRegardsApi.Presentation.DTO.Stickers
{
    public class UnlockStickerDTO
    {
        public long Id {get; set;} = 0L;
        public int Amount {get; set;} = 0;

        // Default constructor
        public UnlockStickerDTO(){}

        public UnlockStickerDTO(long id, int amount)
        {
            this.Id = id;
            this.Amount = amount;
        }
    }
}
