using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using WebAPI.Services.Interface;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<Account> PostAccountAsync(Account account)
        {
            return await _accountRepo.PostAccountAsync(account);
        }

        public async Task<Account> GetAccountAsync(string username)
        {
            return await _accountRepo.GetAccountAsync(username);
        }

        public async Task<Account> GetAccountAsync(string username, string password)
        {
            return await _accountRepo.GetAccountAsync(username, password);
        }

        public async Task DeleteAccountAsync(string username)
        {
            await _accountRepo.DeleteAccountAsync(username);
        }
    }
}