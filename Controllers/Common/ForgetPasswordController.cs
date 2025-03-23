using FoodOrderSystem.Services.Implements;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderSystem.Controllers.Common
{
    public class ForgetPasswordController : Controller
    {
        private readonly IAccountService _accountService;

        public ForgetPasswordController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult ForgotIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordProcess(string email)
        {
            var account = await _accountService.GetAccountInfoByEmailAsync(email);
            if (account == null)
            {
                ViewBag.Error = "Email không tồn tại!";
                return View("ForgotIndex");
            }

            TempData["OTP"] = "123456";
            TempData["Email"] = email;

            return RedirectToAction("VerifyOTP");
        }

        [HttpPost]
        public IActionResult VerifyOTP(string otp)
        {
            var sessionOtp = TempData["OTP"]?.ToString();
            if (otp == sessionOtp)
            {
                TempData.Keep("Email");
                return RedirectToAction("Index", "ResetPassword");
            }

            ViewBag.Error = "Mã OTP không đúng!";
            return View();
        }
    }
}
