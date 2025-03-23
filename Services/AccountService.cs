using FoodOrderSystem.DTOs;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using FoodOrderSystem.Services.Implements;
using FoodOrderSystem.Utils;

namespace FoodOrderSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account?> InsertAccountAsync(string email, string password, string fullname, string phone, int status, int role, string address, DateOnly birth, int gender)
        {
            var account = new Account
            {
                Email = email,
                Password = password,
                Fullname = fullname,
                Phone = phone,
                StatusAcc = status,
                Role = role,
                Address = address,
                Dob = birth,
                Gender = gender
            };

            return await _accountRepository.InsertAccountAsync(account);
        }

        public async Task<Account?> CheckExistedAccountAsync(string email)
        {
            return await _accountRepository.GetAccountInfoByEmailAsync(email);
        }

        public async Task<Account?> GetAccountAsync(string email, string password)
        {
            return await _accountRepository.GetAccountAsync(email, password);
        }

        public async Task<bool> ChangeAccountAsync(string email, string name, string phone, string address, DateOnly birth, int gender)
        {
            return await _accountRepository.ChangeAccountAsync(email, name, phone, address, birth, gender);
        }

        public async Task<Account?> GetAccountInfoByEmailAsync(string email)
        {
            return await _accountRepository.GetAccountInfoByEmailAsync(email);
        }

        public async Task<bool> CheckOldPasswordAsync(int accountId, string oldPassword)
        {
            return await _accountRepository.CheckOldPasswordAsync(accountId, oldPassword);
        }

        public async Task<bool> UpdateAccountPasswordAsync(int accountId, string newPassword)
        {
            return await _accountRepository.UpdateAccountPasswordAsync(accountId, newPassword);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _accountRepository.GetAccountsAsync();
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            return await _accountRepository.UpdateAccountAsync(account);
        }

        public async Task<List<Account>> GetAccountStaffsAsync()
        {
            return await _accountRepository.GetAccountStaffsAsync();
        }

        public async Task<bool> ResetPassword(AccountDTO accountDTO, string newPassword)
        {
            var account = await _accountRepository.GetAccountInfoByEmailAsync(accountDTO.Email);
            if (account == null) return false;

            account.Password = SecurityUtils.HashMd5(newPassword);
            account.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);

            return await _accountRepository.ResetPassword(account);
        }
    }

}
