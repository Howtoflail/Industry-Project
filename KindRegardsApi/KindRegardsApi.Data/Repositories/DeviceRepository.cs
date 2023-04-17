using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KindRegardsApi.Entity;
using KindRegardsApi.Data.Abstractions.Repositories;

namespace KindRegardsApi.Data.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<DeviceEntity> deviceSet;

        public DeviceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.Devices == null)
            {
                throw new MissingFieldException("Cannot find device entities inside database context.");
            }

            this.deviceSet = this.dbContext.Devices;
        }

        public async Task<DeviceEntity?> GetAsync(string id)
        {
            return await this.deviceSet.FindAsync(id);
        }

        public List<DeviceEntity> GetAll()
        {
            return this.deviceSet.ToList();
        }

        public async Task<DeviceEntity> CreateAsync(DeviceEntity entity)
        {
            this.deviceSet.Add(entity);
            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await this.GetAsync(id);

            if (entity == null)
            {
                return false;
            }

            this.deviceSet.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
