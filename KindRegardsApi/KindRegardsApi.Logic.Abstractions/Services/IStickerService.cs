using KindRegardsApi.Domain;
using KindRegardsApi.Domain.Stickers;

namespace KindRegardsApi.Logic.Abstractions.Services
{
    public interface IStickerService
    {
        Task<DeviceSticker?> Get(string deviceId, long id);
        Task<List<DeviceSticker>> GetAll(string deviceId);
        Task<Sticker?> Create(string image);
        Task<Sticker?> Update(Sticker sticker);
        Task<DeviceSticker?> Unlock(string deviceId, long stickerId, int amount);
        Task<bool> Delete(long id);
    }
}
