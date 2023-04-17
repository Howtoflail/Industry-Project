using KindRegardsApi.Entity;

namespace KindRegardsApi.Data.Abstractions.Repositories
{
    public interface IPetRepository
    {
        Task<List<PetEntity>> GetAll();
        Task<PetEntity?> GetAsync(int userId);
        Task<PetEntity> CreateAsync(PetEntity entity);
        Task<PetEntity?> UpdateAsync(PetEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
