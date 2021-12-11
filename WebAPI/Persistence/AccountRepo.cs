using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Persistence.Interface;

namespace WebAPI.Persistence
{
    public class AccountRepo : IAccountRepo
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

            var account = await database.Accounts.Include(a
                    => a.Plants).ThenInclude(p => p.Measurements)
                .FirstOrDefaultAsync(a => a.Username.Equals(username));

            if (account != null)
            {
                foreach (var plant in account.Plants)
                {
                    database.Measurements.RemoveRange(plant.Measurements);
                }

                database.Plants.RemoveRange(account.Plants);
                database.Accounts.Remove(account);

                await database.SaveChangesAsync();
            }
        }
    }
}