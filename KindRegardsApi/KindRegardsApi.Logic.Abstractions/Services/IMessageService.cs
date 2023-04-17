using KindRegardsApi.Domain.Messages;

namespace KindRegardsApi.Logic.Abstractions.Services
{
    public interface IMessageService
    {
        Task<Message?> Get(string deviceId, long id);
        Task<List<Message>> GetAll(string deviceId);
        Task<Message?> Create(string deviceId,string text, DateTime date);
        Task<Message?> Update(Message message,string deviceId);
        Task<bool> Delete(long id);
        Task ThankSender(Message message);
        Task<bool> CheckWhitelist(string message);
    }
}
