using Microsoft.EntityFrameworkCore;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Data.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<GiftEntity> gifts;

        public GiftRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.Gifts == null)
            {
                throw new MissingFieldException("Cannot find gifts entities inside database context.");
            }

            this.gifts = this.dbContext.Gifts;
        }

        public async Task<GiftEntity> CreateAsync(GiftEntity entity)
        {
            this.gifts.Add(entity);
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

            this.gifts.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public List<GiftEntity> GetAll()
        {
            return this.gifts.ToList();
        }

        public async Task<GiftEntity?> GetAsync(long id)
        {
            return await this.gifts.FindAsync(id);
        }

        public async Task<bool> HasWithId(long id)
        {
            var foundGift = await this.gifts.FindAsync(id);
            return foundGift != null;
        }

        public async Task<GiftEntity?> UpdateAsync(GiftEntity entity)
        {
            var existingEntity = await this.gifts.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.StickerId = entity.StickerId;
            this.dbContext.Update<GiftEntity>(existingEntity);

            await this.dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
