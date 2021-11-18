using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public Task<Account> PostAccountAsync(Account account)
        {
            return _accountRepo.PostAccountAsync(account);
        }

        public Task<Account> GetAccountAsync(string username)
        {
            return _accountRepo.GetAccountAsync(username);
        }

        public Task<Account> GetAccountAsync(string username, string password)
        {
            return _accountRepo.GetAccountAsync(username, password);
        }

        public Task<Account> DeleteAccountAsync(string username)
        {
            return _accountRepo.DeleteAccountAsync(username);
        }
    }
}