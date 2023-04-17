using KindRegardsApi.Entity;

namespace KindRegardsApi.Data.Abstractions.Repositories
{
    public interface IDeviceRepository
    {
        Task<DeviceEntity?> GetAsync(string id);
        List<DeviceEntity> GetAll();
        Task<DeviceEntity> CreateAsync(DeviceEntity entity);
        Task<bool> DeleteAsync(string id);
    }
}
