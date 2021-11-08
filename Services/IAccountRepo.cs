using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAccountRepo
    {
        Task<Account> GetAccountAsync();
        Task<Account> PostAccountAsync(Account account);
    }
}