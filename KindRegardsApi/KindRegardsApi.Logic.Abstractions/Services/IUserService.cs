using KindRegardsApi.Domain;


namespace KindRegardsApi.Logic.Abstractions.Services
{
    public interface IUserService
    {
        Task<User?> Create(User user);
        Task<bool> Delete(User user);
        Task<User?> findUser(string computer_code);  
        Task<User?> Get(int id);
        Task<User?> Update(User user);
        Task<List<User>> GetAll();
    }
}
