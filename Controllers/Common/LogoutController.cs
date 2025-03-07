using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace FoodOrderSystem.Controllers.Common
{
    public class LogoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Xóa toàn bộ session
                HttpContext.Session.Clear();

                // Xóa các cookie liên quan đến selector và cart
                if (Request.Cookies.Count > 0)
                {
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        if (cookie == "selector" || cookie == "cart")
                        {
                            Response.Cookies.Delete(cookie);
                        }
                    }
                }

                // Chuyển hướng về trang Login
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error at LogoutController: {ex.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
