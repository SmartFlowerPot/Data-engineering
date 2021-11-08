using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AccountRepo: IAccountRepo
    {
        public Task<Account> GetAccountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Account> PostAccountAsync(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}