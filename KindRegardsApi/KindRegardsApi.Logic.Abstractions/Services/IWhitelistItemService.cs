using KindRegardsApi.Domain.Messages;

namespace KindRegardsApi.Logic.Abstractions.Services
{
   public interface IWhitelistItemService
    {
        Task<WhitelistItem?> Get(long id);
        Task<List<WhitelistItem>> GetAll();
        Task<bool> IsWhitelisted(string text);
        Task<WhitelistItem?> Create( string text);
        Task<WhitelistItem?> Update(WhitelistItem whitelistItem);
        Task<bool> Delete(long id);
    }
}
