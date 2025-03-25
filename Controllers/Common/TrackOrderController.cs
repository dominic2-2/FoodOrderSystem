using Microsoft.AspNetCore.Mvc;
using FoodOrderSystem.Services.Implements;
using FoodOrderSystem.DTOs;

namespace FoodOrderSystem.Controllers.Common
{
    public class TrackOrderController : Controller
    {
        private readonly IOrderService _orderService;

        public TrackOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        [HttpPost]
        public IActionResult ApproveOrder(int orderId)
        {
            var order = _orderService.GetOrderById(orderId) as OrderDTO;
            if (order != null && order.StatusOrder == 0) // Chỉ duyệt nếu trạng thái là "Chưa duyệt"
            {
                order.StatusOrder = 1; // Cập nhật thành "Đã duyệt"
                _orderService.UpdateOrder(order); // Cập nhật trong cơ sở dữ liệu
            }

            return RedirectToAction("Index");
        }

    }
}
