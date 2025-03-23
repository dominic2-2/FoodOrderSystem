using FoodOrderSystem.DTOs;
using FoodOrderSystem.Services.Implements;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderSystem.Controllers.Common
{
    public class ResetPasswordController : Controller
    {
        private readonly IAccountService _accountService;

        public ResetPasswordController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeForgotPassword(string accountId, string newPassword)
        {
            var accountDTO = new AccountDTO
            {
                AccountId = int.Parse(accountId),
                // Các thông tin khác nếu cần
            };

            var result = await _accountService.ResetPassword(accountDTO, newPassword);
            if (result)
                return RedirectToAction("PasswordChangedConfirmation");

            ViewBag.Error = "Đổi mật khẩu thất bại!";
            return View("Index");
        }

        public IActionResult PasswordChangedConfirmation()
        {
            return View();
        }
    }
}
