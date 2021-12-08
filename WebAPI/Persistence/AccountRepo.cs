using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public class AccountRepo: IAccountRepo
    {
        public async Task<Account> PostAccountAsync(Account account)
        {
            await using var database = new Database();
            var first = await database.Accounts.FirstOrDefaultAsync(u => u.Username.Equals(account.Username));

            if (first != null) throw new Exception(Status.UserAlreadyExists);

            await database.Accounts.AddAsync(account);
            await database.SaveChangesAsync();

            return account;
        }

        public async Task<Account> GetAccountAsync(string username)
        {
            await using var database = new Database();
            
            var first = await database
                .Accounts
                .Include(a => a.Plants)
                .FirstOrDefaultAsync(u => u.Username.Equals(username));
            if (first == null)
            { 
                throw new Exception(Status.UserNotFound);
            }
            return first;
        }

        public async Task<Account> GetAccountAsync(string username, string password)
        {
            await using var database = new Database();
            
            var first = await database
                .Accounts
                .FirstOrDefaultAsync(u => u.Username.Equals(username));
            
            if (first == null)
            { 
               throw new Exception(Status.UserNotFound);
            }
            if (!first.Password.Equals(password))
            {
                throw new Exception(Status.IncorrectPassword);
            }
            return first;
        }

        public async Task DeleteAccountAsync(string username)
        {
            await using var database = new Database();

            var account = database
                .Accounts
                .Where(a => a.Username.Equals(username))
                .Include(a => a.Plants)
                .FirstAsync();
            
            if (account == null) throw new Exception(Status.UserNotFound);
            
            database.Plants.RemoveRange(account.Result.Plants);
            database.Accounts.Remove(await account);
            await database.SaveChangesAsync();
        }
        
        
    }
    
}