using Microsoft.EntityFrameworkCore;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<MessageEntity> messages;

        public MessageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.Messages == null)
            {
                throw new MissingFieldException("Cannot find message entities inside database context.");
            }

            this.messages = this.dbContext.Messages;
        }

        public async Task<MessageEntity?> GetAsync(long id)
        {
            return await this.messages.FindAsync(id);
        }

        // Personal inbox for a player
        public List<MessageEntity> GetAll()
        {
            return this.messages.ToList();
        }

        public async Task<bool> HasWithId(long id)
        {
            var foundMessage = await this.messages.FindAsync(id);
            return foundMessage != null;
        }

        public async Task<MessageEntity> CreateAsync(MessageEntity entity)
        {
            this.messages.Add(entity);
            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<MessageEntity?> UpdateAsync(MessageEntity entity)
        {
            var existingEntity = await this.messages.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.Text = entity.Text;
            existingEntity.Read=entity.Read;
            existingEntity.Gift = entity.Gift;
            this.dbContext.Update<MessageEntity>(existingEntity);

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

            this.messages.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
