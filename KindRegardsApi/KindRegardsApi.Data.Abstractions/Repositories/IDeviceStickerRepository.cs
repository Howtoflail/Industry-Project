using KindRegardsApi.Entity.Stickers;

namespace KindRegardsApi.Data.Abstractions.Repositories
{
    public interface IDeviceStickerRepository : IRepository<DeviceStickerEntity>
    {
        Task<DeviceStickerEntity?> GetByStickerAndDeviceId(string deviceId, long stickerId);
    }
}
