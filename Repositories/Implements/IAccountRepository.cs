using FoodOrderSystem.Models;

namespace FoodOrderSystem.Repositories.Implements
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountInfoByEmailAsync(string email);
        Task<Account?> InsertAccountAsync(Account account);
        Task<Account?> GetAccountAsync(string email, string password);
        Task<List<Account>> GetAccountsAsync();
        Task<bool> UpdateAccountAsync(Account account);
        Task<bool> ChangeAccountAsync(string email, string name, string phone, string address, DateOnly birth, int gender);
        Task<bool> CheckOldPasswordAsync(int accountId, string oldPassword);
        Task<bool> UpdateAccountPasswordAsync(int accountId, string newPassword);
        Task<List<Account>> GetAccountStaffsAsync();
    }
}
