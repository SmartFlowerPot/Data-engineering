using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interface
{
    public interface IAccountService
    {
        Task<Account> PostAccountAsync(Account account);
        Task<Account> GetAccountAsync(string username);
        Task<Account> GetAccountAsync(string username, string password);
        Task DeleteAccountAsync(string username);
    }
}