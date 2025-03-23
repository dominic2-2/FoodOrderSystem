using FoodOrderSystem.DTOs;
using FoodOrderSystem.Models;

namespace FoodOrderSystem.Services.Implements
{
    public interface IAccountService
    {
        Task<Account?> InsertAccountAsync(string email, string password, string fullname, string phone, int status, int role, string address, DateOnly birth, int gender);
        Task<Account?> CheckExistedAccountAsync(string email);
        Task<Account?> GetAccountAsync(string email, string password);
        Task<bool> ChangeAccountAsync(string email, string name, string phone, string address, DateOnly birth, int gender);
        Task<Account?> GetAccountInfoByEmailAsync(string email);
        Task<bool> CheckOldPasswordAsync(int accountId, string oldPassword);
        Task<bool> UpdateAccountPasswordAsync(int accountId, string newPassword);
        Task<List<Account>> GetAccountsAsync();
        Task<bool> UpdateAccountAsync(Account account);
        Task<List<Account>> GetAccountStaffsAsync();
        Task<bool> ResetPassword(AccountDTO accountDTO, string newPassword);
    }
}
