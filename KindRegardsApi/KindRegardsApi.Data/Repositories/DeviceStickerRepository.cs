using Microsoft.EntityFrameworkCore;

using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Stickers;

namespace KindRegardsApi.Data.Repositories
{
    public class DeviceStickerRepository : IDeviceStickerRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<DeviceStickerEntity> deviceStickerSet;

        public DeviceStickerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.DeviceStickers == null)
            {
                throw new MissingFieldException("Cannot find sticker entities inside database context.");
            }

            this.deviceStickerSet = this.dbContext.DeviceStickers;
        }

        public async Task<DeviceStickerEntity?> GetAsync(long id)
        {
            return await this.deviceStickerSet.Where(ds => ds.Id == id)
                                              .Include(ds => ds.Device)
                                              .Include(ds => ds.Sticker)
                                              .FirstOrDefaultAsync<DeviceStickerEntity>();
        }

        public List<DeviceStickerEntity> GetAll()
        {
            return this.deviceStickerSet.ToList();
        }

        public async Task<DeviceStickerEntity?> GetByStickerAndDeviceId(string deviceId, long stickerId)
        {
            return await this.deviceStickerSet.Where(ds => ds.Device != null && ds.Device.Id == deviceId)
                                              .Where(ds => ds.Sticker != null && ds.Sticker.Id == stickerId)
                                              .Include(ds => ds.Device)
                                              .Include(ds => ds.Sticker)
                                              .FirstOrDefaultAsync();
        }

        public async Task<bool> HasWithId(long id)
        {
            var foundDeviceSticker = await this.deviceStickerSet.FindAsync(id);
            return foundDeviceSticker != null;
        }

        public async Task<DeviceStickerEntity> CreateAsync(DeviceStickerEntity entity)
        {
            this.deviceStickerSet.Add(entity);
            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<DeviceStickerEntity?> UpdateAsync(DeviceStickerEntity entity)
        {
            var existingEntity = await this.deviceStickerSet.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                return null;
            }

            // Only update the amount
            existingEntity.Amount = entity.Amount;

            this.dbContext.Update<DeviceStickerEntity>(existingEntity);

            await this.dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await this.GetAsync(id);

            if (entity == null)
            {
                return false;
            }

            this.deviceStickerSet.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
