using FoodOrderSystem.Data;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FoodOrderSystemContext _context;

        public AccountRepository(FoodOrderSystemContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountInfoByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Account?> InsertAccountAsync(Account account)
        {
            account.CreateTime = TimeOnly.FromDateTime(DateTime.Now);
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetAccountAsync(string email, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            account.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);
            _context.Accounts.Update(account);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeAccountAsync(string email, string name, string phone, string address, DateOnly birth, int gender)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            if (account == null) return false;

            account.Fullname = name;
            account.Phone = phone;
            account.Address = address;
            account.Dob = birth;
            account.Gender = gender;
            account.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);

            _context.Accounts.Update(account);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CheckOldPasswordAsync(int accountId, string oldPassword)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
            return account != null && account.Password == oldPassword;
        }

        public async Task<bool> UpdateAccountPasswordAsync(int accountId, string newPassword)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
            if (account == null) return false;

            account.Password = newPassword;
            account.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);
            _context.Accounts.Update(account);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Account>> GetAccountStaffsAsync()
        {
            return await _context.Accounts
                .Where(a => a.Role == 1 && a.Staff.Any(s => s.AccountId == a.AccountId && s.Role == 3))
                .ToListAsync();
        }

        public async Task<bool> ResetPassword(Account account)
        {
            _context.Accounts.Update(account);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
