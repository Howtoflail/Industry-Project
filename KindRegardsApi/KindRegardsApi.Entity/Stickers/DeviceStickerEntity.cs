namespace KindRegardsApi.Entity.Stickers
{
    public class DeviceStickerEntity
    {
        public long Id {get; set;} = 0;
        public DeviceEntity? Device {get; set;} = null;
        public StickerEntity? Sticker {get; set;} = null;
        public int Amount {get; set;} = 0;

        // Default constructor
        public DeviceStickerEntity(){}

        public DeviceStickerEntity(long id, DeviceEntity device, StickerEntity sticker, int amount)
        {
            this.Id = id;
            this.Device = device;
            this.Sticker = sticker;
            this.Amount = amount;
        }
    }
}
