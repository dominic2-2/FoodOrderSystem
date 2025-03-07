using FoodOrderSystem.Services;
using FoodOrderSystem.Models;
using FoodOrderSystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FoodOrderSystem.Utils;
using System.Threading.Tasks;
using System;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Controllers.Client
{
    public class UserHomeController : Controller
    {
        private readonly IAccountService _accountService;

        public UserHomeController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        private bool IsCustomer()
        {
            return HttpContext.Session.GetString("ROLE") == "Customer";
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsCustomer())
            {
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString("destPage", "user");
            return View("UserHome");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string action, string name, string phone, string address, string birth, int gender, string oldPassword, string newPassword)
        {
            if (!IsCustomer())
            {
                return RedirectToAction("Index", "Login");
            }

            var userEmail = HttpContext.Session.GetString("LOGIN_USER");

            try
            {
                Account? account = await _accountService.GetAccountInfoByEmailAsync(userEmail);
                if (account == null)
                {
                    ViewData["MSG_ERROR"] = "User not found!";
                    return View("UserHome");
                }

                if (action == "updateInfo")
                {
                    DateOnly birthDate;
                    if (!DateOnly.TryParse(birth, out birthDate))
                    {
                        ViewData["MSG_ERROR"] = "Invalid birth date format!";
                        return View("UserHome");
                    }

                    bool updated = await _accountService.ChangeAccountAsync(userEmail, name, phone, address, birthDate, gender);

                    if (updated)
                    {
                        account = await _accountService.GetAccountInfoByEmailAsync(userEmail);
                        HttpContext.Session.SetString("LOGIN_USER", account?.Email ?? "");
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
                        return View("UserHome");
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

                return View("UserHome");
            }
            catch (Exception ex)
            {
                ViewData["MSG_ERROR"] = $"Error: {ex.Message}";
                return View("UserHome");
            }
        }
    }
}
