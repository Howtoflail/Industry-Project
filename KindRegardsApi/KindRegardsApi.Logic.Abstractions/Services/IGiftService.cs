using KindRegardsApi.Domain.Messages;

namespace KindRegardsApi.Logic.Abstractions.Services
{
    public interface IGiftService
    {
        Task<Gift?> Get(long id);
        Task<List<Gift>> GetAll();
        Task<Gift?> Create(long stickerId);
        Task<Gift?> Update(Gift gift);
        Task<bool> Delete(long id);
    }
}
