using FoodOrderSystem.Services;
using FoodOrderSystem.Models;
using FoodOrderSystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FoodOrderSystem.Utils;
using System.Threading.Tasks;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IStaffService _staffService;

        private const int STAFF = 1;
        private const int CUSTOMER = 0;
        private const int ADMIN = 0;
        private const int MANAGER = 1;
        private const int SHIPPER = 2;
        private const int SELLER = 3;

        public LoginController(IAccountService accountService, IStaffService staffService)
        {
            _accountService = accountService;
            _staffService = staffService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string email, string password, string remember)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                HttpContext.Session.SetString("destPage", "home");
                return RedirectToAction("Index", "Home");
            }

            try
            {
                string hashedPassword = SecurityUtils.HashMd5(password);
                Account? account = await _accountService.GetAccountAsync(email, hashedPassword);

                if (account == null)
                {
                    ViewData["email"] = email;
                    ViewData["ERROR_MESSAGE"] = "Incorrect email or password!";
                    return View("Login");
                }

                if (account.StatusAcc == 0)
                {
                    ViewData["ERROR_MESSAGE"] = "Your account has been locked! Please contact admin.";
                    return View("Login");
                }

                HttpContext.Session.SetString("LOGIN_USER", account.Email ?? "");

                if (account.Role == STAFF)
                {
                    return await HandleStaffLogin(account.AccountId);
                }
                else if (account.Role == CUSTOMER)
                {
                    HttpContext.Session.SetString("ROLE", "Customer");
                    HttpContext.Session.SetString("destPage", "user");
                    return RedirectToAction("Index","UserHome");
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Your role is not supported!";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"An error occurred: {ex.Message}";
                return View("Login");
            }
        }

        private async Task<IActionResult> HandleStaffLogin(int accountId)
        {
            StaffDTO? staff = await _staffService.GetStaffByAccountIdAsync(accountId);

            if (staff == null)
            {
                ViewData["ERROR_MESSAGE"] = "Staff information not found!";
                return View("Login");
            }

            switch (staff.Role)
            {
                case ADMIN:
                    HttpContext.Session.SetString("ROLE", "Admin");
                    HttpContext.Session.SetString("destPage", "admin");
                    return RedirectToAction("Index", "AdminManageAccount");
                case MANAGER:
                    HttpContext.Session.SetString("ROLE", "Manager");
                    HttpContext.Session.SetString("destPage", "manager");
                    return RedirectToAction("Index", "ManagerManageSeller");
                case SHIPPER:
                    HttpContext.Session.SetString("ROLE", "Shipper");
                    HttpContext.Session.SetString("destPage", "shipper");
                    return RedirectToAction("Index", "ShipperManageOrder");
                case SELLER:
                    HttpContext.Session.SetString("ROLE", "Seller");
                    HttpContext.Session.SetString("destPage", "seller");
                    return RedirectToAction("Index", "SellerManageProduct");
                default:
                    ViewData["ERROR_MESSAGE"] = "Invalid staff role!";
                    return View("Login");
            }
        }
    }
}
