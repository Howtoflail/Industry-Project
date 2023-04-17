using Microsoft.EntityFrameworkCore;

using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Stickers;

namespace KindRegardsApi.Data.Repositories
{
    public class StickerRepository : IStickerRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<StickerEntity> stickerSet;

        public StickerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.Stickers == null)
            {
                throw new MissingFieldException("Cannot find sticker entities inside database context.");
            }

            this.stickerSet = this.dbContext.Stickers;
        }

        public async Task<StickerEntity?> GetAsync(long id)
        {
            return await this.stickerSet.FindAsync(id);
        }

        public List<StickerEntity> GetAll()
        {
            return this.stickerSet.ToList();
        }

        public async Task<bool> HasWithId(long id)
        {
            var foundSticker = await this.stickerSet.FindAsync(id);
            return foundSticker != null;
        }

        public async Task<StickerEntity> CreateAsync(StickerEntity entity)
        {
            this.stickerSet.Add(entity);
            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<StickerEntity?> UpdateAsync(StickerEntity entity)
        {
            var existingEntity = await this.stickerSet.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.Image = entity.Image;
            this.dbContext.Update<StickerEntity>(existingEntity);

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

            this.stickerSet.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
