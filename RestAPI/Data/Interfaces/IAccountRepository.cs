using RestAPI.Models;

namespace RestAPI.Data.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        int? Login(string email, string password);
    }
}