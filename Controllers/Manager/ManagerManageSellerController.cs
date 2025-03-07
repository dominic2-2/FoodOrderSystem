using FoodOrderSystem.Models;
using FoodOrderSystem.Services.Implements;
using FoodOrderSystem.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodOrderSystem.Controllers.Manager
{
    public class ManagerManageSellerController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IStaffService _staffService;
        private readonly IEmailService _emailService;

        public ManagerManageSellerController(IAccountService accountService, IStaffService staffService, IEmailService emailService)
        {
            _accountService = accountService;
            _staffService = staffService;
            _emailService = emailService;
        }

        private bool IsAuthorized()
        {
            return HttpContext.Session.GetString("ROLE") == "Manager";
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
                List<Account> sellerAccounts = await _accountService.GetAccountStaffsAsync();
                ViewData["listAccounts"] = sellerAccounts;
                return View("ManageSellerPage");
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return View("ManageSellerPage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSellerAccount(string email, string name, string phone, string birthdate, string address, int gender)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                DateOnly birth = DateOnly.Parse(birthdate);
                string generatedPassword = SecurityUtils.GenerateRandomPassword(6);
                string hashedPassword = SecurityUtils.HashMd5(generatedPassword);

                var existingAccount = await _accountService.CheckExistedAccountAsync(email);
                if (existingAccount != null)
                {
                    ViewData["ERROR_MESSAGE"] = "Tài khoản đã tồn tại! Vui lòng dùng email khác.";
                    return await Index();
                }

                var newAccount = await _accountService.InsertAccountAsync(email, hashedPassword, name, phone, 1, 1, address, birth, gender);
                bool isSellerCreated = await _staffService.InsertStaffAsync(newAccount.AccountId, 3);

                if (isSellerCreated)
                {
                    string emailBody = $"<p>Dear {name},</p><p>Your Password is: <strong>{generatedPassword}</strong></p><p>Best regards,<br/>FOS Admin.</p>";
                    await _emailService.SendEmailAsync(email, "[Show Room] YOUR PASSWORD", emailBody);

                    ViewData["MSG_SUCCESS"] = "Đăng ký tài khoản thành công!";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Đã xảy ra lỗi, vui lòng thử lại.";
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
        public async Task<IActionResult> ChangeSellerStatus(string email)
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
