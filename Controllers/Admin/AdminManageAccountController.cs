using FoodOrderSystem.Services;
using FoodOrderSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Controllers.Admin
{
    public class AdminManageAccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AdminManageAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        private bool IsAuthorized()
        {
            return HttpContext.Session.GetString("ROLE") == "Admin";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                List<Account> listAccounts = await _accountService.GetAccountsAsync();
                ViewData["listAccounts"] = listAccounts;
                return View("ManageCustomerAccountPage");
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return View("ManageCustomerAccountPage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(int accountId, string name, string phone, string address, string birth, int gender)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                DateOnly birthDate = DateOnly.Parse(birth);
                List<Account> accounts = await _accountService.GetAccountsAsync();
                Account? account = accounts.FirstOrDefault(a => a.AccountId == accountId);

                if (account == null)
                {
                    ViewData["ERROR_MESSAGE"] = "Account not found!";
                    return await Index();
                }

                bool updated = await _accountService.ChangeAccountAsync(account.Email, name, phone, address, birthDate, gender);
                if (updated)
                {
                    ViewData["MSG_SUCCESS"] = "Account updated successfully!";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Failed to update account!";
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return await Index();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                Account? account = await _accountService.GetAccountInfoByEmailAsync(accountId.ToString());
                if (account == null)
                {
                    ViewData["ERROR_MESSAGE"] = "Account not found!";
                    return await Index();
                }

                bool deleted = await _accountService.UpdateAccountAsync(account); // Thay thế bằng phương thức xóa nếu có
                if (deleted)
                {
                    ViewData["MSG_SUCCESS"] = "Account deleted successfully!";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Failed to delete account!";
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return await Index();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(string email)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    ViewData["ERROR_MESSAGE"] = "Email is required!";
                    return await Index();
                }

                Account? account = await _accountService.GetAccountInfoByEmailAsync(email);
                if (account == null)
                {
                    ViewData["ERROR_MESSAGE"] = "Account not found!";
                    return await Index();
                }

                // Toggle trạng thái tài khoản
                account.StatusAcc = account.StatusAcc == 1 ? 0 : 1;
                bool updated = await _accountService.UpdateAccountAsync(account);

                if (updated)
                {
                    ViewData["MSG_SUCCESS"] = account.StatusAcc == 1 ? "Account unblocked successfully." : "Account blocked successfully.";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Failed to update account status!";
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return await Index();
            }
        }
    }
}
