using KindRegardsApi.Entity;
using KindRegardsApi.Entity.user;

namespace KindRegardsApi.Data.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetAsync(int id);
        Task<UserEntity?> GetAsyncComputerCode(string computer_code);
        Task<UserEntity> CreateAsync(UserEntity entity);
        Task<UserEntity?> UpdateAsync(UserEntity entity);
        Task<bool> DeleteAsync(UserEntity entity);
        List<UserEntity> GetAll();
    }
}
