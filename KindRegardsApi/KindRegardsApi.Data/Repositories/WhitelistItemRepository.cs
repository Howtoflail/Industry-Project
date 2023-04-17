using Microsoft.EntityFrameworkCore;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Data.Repositories
{
    public class WhitelistItemRepository : IWhitelistItemRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<WhitelistItemEntity> whitelist;
        public WhitelistItemRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.whitelist == null)
            {
                throw new MissingFieldException("Cannot find whitelist item entities inside database context.");
            }

            this.whitelist = this.dbContext.whitelist;
        }

        public async Task<WhitelistItemEntity> CreateAsync(WhitelistItemEntity entity)
        {
            this.whitelist.Add(entity);
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

            this.whitelist.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public List<WhitelistItemEntity> GetAll()
        {
            return this.whitelist.ToList();
        }

        public async Task<WhitelistItemEntity?> GetAsync(long id)
        {
            return await this.whitelist.FindAsync(id);
        }

        public async Task<bool> HasWithId(long id)
        {
            var foundWhitelistItem = await this.whitelist.FindAsync(id);
            return foundWhitelistItem != null;
        }

        public async Task<WhitelistItemEntity?> UpdateAsync(WhitelistItemEntity entity)
        {
            var existingEntity = await this.whitelist.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.Text = entity.Text;
            this.dbContext.Update<WhitelistItemEntity>(existingEntity);

            await this.dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
