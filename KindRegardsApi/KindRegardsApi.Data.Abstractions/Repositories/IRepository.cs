using System.Threading.Tasks;

namespace KindRegardsApi.Data.Abstractions.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity?> GetAsync(long id);
        List<TEntity> GetAll();
        Task<bool> HasWithId(long id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(long id);
    }
}
