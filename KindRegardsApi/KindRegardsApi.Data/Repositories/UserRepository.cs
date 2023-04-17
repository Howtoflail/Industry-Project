using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Messages;
using KindRegardsApi.Entity.user;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindRegardsApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        
        private ApplicationDbContext dbContext;
        private DbSet<UserEntity> user;
        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (this.dbContext.user == null)
            {
                throw new MissingFieldException("Cannot find users entities inside database context.");
            }
            this.user = this.dbContext.user;
        }

        public async Task<UserEntity?> CreateAsync(UserEntity user)
        {
            this.user.Add(user);
            await this.dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteAsync(UserEntity user)
        {
            var entity = await this.GetAsync(Convert.ToInt16(user.id));

            if (entity == null)
            {
                return false;
            }

            this.user.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<UserEntity?> GetAsync(int id)
        {
            return await this.user.FindAsync(id);
        }
        public async Task<UserEntity?> GetAsyncComputerCode(string computer_code)
        {
            return await this.user.Where(x => x.computer_code == computer_code).SingleOrDefaultAsync();
        }

        public async Task<UserEntity?> UpdateAsync(UserEntity entity)
        {
            var existingEntity = await this.user.FindAsync(entity.id);

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.id = entity.id;
            this.dbContext.Update<UserEntity>(existingEntity);

            await this.dbContext.SaveChangesAsync();
            return entity;
        }
        public List<UserEntity> GetAll()
        {
            return this.user.ToList();
        }


    }
}
