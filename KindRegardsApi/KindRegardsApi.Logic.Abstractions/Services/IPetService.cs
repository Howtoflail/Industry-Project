using KindRegardsApi.Domain;

namespace KindRegardsApi.Logic.Abstractions.Services
{
    public interface IPetService
    {
        Task<List<Pet>> GetAll();
        Task<Pet?> Get(int userId);
        Task<Pet?> Create(Pet pet, int userId);
        Task<Pet?> Update(Pet pet, int userId);
        Task<bool> Delete(int userId);
    }
}
