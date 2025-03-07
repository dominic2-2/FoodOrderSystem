using FoodOrderSystem.Models;
using FoodOrderSystem.Services.Implements;
using FoodOrderSystem.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodOrderSystem.Controllers.Staff
{
    public class StaffInfoController : Controller
    {
        private readonly IAccountService _accountService;

        public StaffInfoController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userEmail = HttpContext.Session.GetString("LOGIN_USER");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Index", "Login");
            }

            var account = await _accountService.GetAccountInfoByEmailAsync(userEmail);
            ViewData["account"] = account;

            return View("StaffInfo");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStaffInfo(string action, string name, string phone, string address, string birth, int gender, string oldPassword, string newPassword)
        {
            var userEmail = HttpContext.Session.GetString("LOGIN_USER");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                Account? account = await _accountService.GetAccountInfoByEmailAsync(userEmail);
                if (account == null)
                {
                    ViewData["MSG_ERROR"] = "User not found!";
                    return await Index();
                }

                if (action == "updateInfo")
                {
                    DateOnly birthDate;
                    if (!DateOnly.TryParse(birth, out birthDate))
                    {
                        ViewData["MSG_ERROR"] = "Invalid birth date format!";
                        return await Index();
                    }

                    bool updated = await _accountService.ChangeAccountAsync(userEmail, name, phone, address, birthDate, gender);

                    if (updated)
                    {
                        account = await _accountService.GetAccountInfoByEmailAsync(userEmail);
                        HttpContext.Session.SetString("LOGIN_USER", account.Email ?? "");
                        ViewData["MSG_SUCCESS"] = "Update profile information successfully!";
                    }
                    else
                    {
                        ViewData["MSG_ERROR"] = "Oops! Something went wrong! Try later!";
                    }
                }
                else if (action == "changePassword")
                {
                    if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                    {
                        ViewData["MSG_ERROR"] = "Old and new passwords are required!";
                        return await Index();
                    }

                    string hashedOldPassword = SecurityUtils.HashMd5(oldPassword);
                    bool isOldPasswordValid = await _accountService.CheckOldPasswordAsync(account.AccountId, hashedOldPassword);

                    if (isOldPasswordValid)
                    {
                        string hashedNewPassword = SecurityUtils.HashMd5(newPassword);
                        bool passwordUpdated = await _accountService.UpdateAccountPasswordAsync(account.AccountId, hashedNewPassword);

                        if (passwordUpdated)
                        {
                            ViewData["MSG_SUCCESS"] = "Change password successfully!";
                        }
                        else
                        {
                            ViewData["MSG_ERROR"] = "Could not update password!";
                        }
                    }
                    else
                    {
                        ViewData["MSG_ERROR"] = "You entered the wrong old password! Please try again!";
                    }
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["MSG_ERROR"] = $"Error: {ex.Message}";
                return await Index();
            }
        }
    }
}
