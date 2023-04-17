namespace KindRegardsApi.Domain.Stickers
{
    public class DeviceSticker
    {
        public long Id {get; set;} = 0L;
        public string DeviceId {get; set;} = "";
        public Sticker? Sticker {get; set;} = null;
        public int Amount {get; set;} = 0;

        // Default constructor
        public DeviceSticker(){}

        public DeviceSticker(long id, string deviceId, Sticker sticker, int amount)
        {
            this.Id = id;
            this.DeviceId = deviceId;
            this.Sticker = sticker;
            this.Amount = amount;
        }
    }
}
