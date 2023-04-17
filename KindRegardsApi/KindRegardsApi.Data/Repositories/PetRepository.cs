using Microsoft.EntityFrameworkCore;

using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity;

namespace KindRegardsApi.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<PetEntity> petSet;

        public PetRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.pet == null)
            {
                throw new MissingFieldException("Cannot find pets entities inside database context.");
            }

            this.petSet = this.dbContext.pet;
        }

        public async Task<List<PetEntity>> GetAll()
        {
            return this.petSet.ToList();
        }

        public async Task<PetEntity?> GetAsync(int userId)
        {
            return await this.petSet.Where(x => x.userId == userId).SingleOrDefaultAsync();
        }

        public async Task<PetEntity> CreateAsync(PetEntity entity)
        {
            this.petSet.Add(entity);
            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<PetEntity?> UpdateAsync(PetEntity entity)
        {
            var existingEntity = await this.petSet.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.petKind = entity.petKind;
            existingEntity.Name = entity.Name;
            existingEntity.Colour = entity.Colour;

            this.dbContext.Update<PetEntity>(existingEntity);

            await this.dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await this.GetAsync(id);

            if (entity == null)
            {
                return false;
            }

            this.petSet.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
