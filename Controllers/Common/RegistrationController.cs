using FoodOrderSystem.Services.Implements;
using FoodOrderSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderSystem.Controllers.Common
{
    public class RegistrationController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;

        public RegistrationController(IAccountService accountService, ICustomerService customerService)
        {
            _accountService = accountService;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("RegistrationPage");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string name, string phone, string password, string birthdate, string address, bool gender)
        {
            try
            {
                DateOnly birth = DateOnly.Parse(birthdate);
                string hashedPassword = SecurityUtils.HashMd5(password);

                // Kiểm tra tài khoản đã tồn tại hay chưa
                var existingAccount = await _accountService.CheckExistedAccountAsync(email);
                if (existingAccount != null)
                {
                    ViewData["MSG_ERROR"] = "Tài khoản đã tồn tại! Vui lòng đăng nhập bằng email này.";
                    return View("RegistrationPage");
                }

                // Tạo tài khoản mới
                var newAccount = await _accountService.InsertAccountAsync(email, hashedPassword, name, phone, 1, 0, address, birth, gender ? 1 : 0);
                bool isCustomerCreated = await _customerService.InsertCustomerAsync(newAccount.AccountId, 0);

                if (isCustomerCreated)
                {
                    ViewData["MSG_SUCCESS"] = "Đăng ký tài khoản thành công!";
                    return RedirectToAction("Index", "Login");
                }

                ViewData["MSG_ERROR"] = "Đã xảy ra lỗi trong quá trình đăng ký, vui lòng thử lại.";
                return View("RegistrationPage");
            }
            catch (Exception ex)
            {
                ViewData["MSG_ERROR"] = $"Lỗi: {ex.Message}";
                return View("RegistrationPage");
            }
        }
    }
}
