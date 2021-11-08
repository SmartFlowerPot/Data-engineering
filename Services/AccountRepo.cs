using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AccountRepo: IAccountRepo
    {
        public async Task<Account> PostAccountAsync(Account account)
        {
            using (var database = new Database())
            {
                Account first = await database.Accounts.FirstOrDefaultAsync(u => u.Username.Equals(account.Username));

                if (first != null) throw new Exception("Username already register. Choose another username or login with your account.");

                await database.Accounts.AddAsync(account);
                await database.SaveChangesAsync();
            }

            return account;
        }

        public Task<Account> GetAccountAsync()
        {
            throw new NotImplementedException();
        }
    }
}