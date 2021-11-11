using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAccountService
    {
        Task<Account> PostAccountAsync(Account account);
        Task<Account> GetAccountAsync(string username);
        Task<Account> GetAccountAsync(string username, string password);
    }
}