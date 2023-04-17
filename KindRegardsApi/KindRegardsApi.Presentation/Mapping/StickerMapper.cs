using KindRegardsApi.Presentation.DTO.Stickers;
using KindRegardsApi.Domain.Stickers;

namespace KindRegardsApi.Presentation.Mapping
{
    public class StickerMapper
    {
        public StickerDTO ToDTO(DeviceSticker deviceSticker)
        {
            if (deviceSticker.Sticker == null)
            {
                throw new ArgumentNullException("Could not map device sticker since it does not reference any stickers");
            }

            return new StickerDTO(
                deviceSticker.Sticker.Id,
                deviceSticker.Sticker.Image,
                deviceSticker.Amount,
                deviceSticker.DeviceId != null && deviceSticker.DeviceId.Length > 0
            );
        }

        public List<StickerDTO> toDTOS(List<DeviceSticker> deviceStickers)
        {
            var dtos = new List<StickerDTO>();

            foreach(DeviceSticker deviceSticker in deviceStickers)
            {
                dtos.Add(this.ToDTO(deviceSticker));
            }

            return dtos;
        }
    }
}
