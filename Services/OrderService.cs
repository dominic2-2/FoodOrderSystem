using FoodOrderSystem.DTOs;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            return new OrderDTO
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Note = order.Note,
                TotalPrice = order.TotalPrice ?? 0,
                StatusOrder = order.StatusOrder ?? 0,
                StatusPayment = order.StatusPayment ?? 0,
                CustomerId = order.CustomerId ?? 0
            };
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Note = o.Note,
                TotalPrice = o.TotalPrice ?? 0,
                StatusOrder = o.StatusOrder ?? 0,
                StatusPayment = o.StatusPayment ?? 0,
                CustomerId = o.CustomerId ?? 0
            }).ToList();
        }

        public async Task<List<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Note = o.Note,
                TotalPrice = o.TotalPrice ?? 0,
                StatusOrder = o.StatusOrder ?? 0,
                StatusPayment = o.StatusPayment ?? 0,
                CustomerId = o.CustomerId ?? 0
            }).ToList();
        }

        public async Task<int> InsertOrderAsync(OrderDTO orderDTO)
        {
            var order = new Order
            {
                OrderDate = orderDTO.OrderDate,
                Note = orderDTO.Note,
                TotalPrice = orderDTO.TotalPrice,
                StatusOrder = orderDTO.StatusOrder,
                StatusPayment = orderDTO.StatusPayment,
                CustomerId = orderDTO.CustomerId
            };

            return await _orderRepository.InsertOrderAsync(order);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, int statusOrder)
        {
            return await _orderRepository.UpdateOrderStatusAsync(orderId, statusOrder);
        }

        public async Task<bool> UpdatePaymentStatusAsync(int orderId, int statusPayment)
        {
            return await _orderRepository.UpdatePaymentStatusAsync(orderId, statusPayment);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
        public List<OrderDTO> GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders(); // gọi từ repo
            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                CustomerId = o.Customer.CustomerId,
                OrderDate = o.OrderDate ?? DateOnly.FromDateTime(System.DateTime.Now), // Fix for CS0266 and CS8629
                TotalPrice = o.TotalPrice ?? 0, // Fix for CS0117 and CS1061
                StatusOrder = o.StatusOrder ?? 0, // Fix for CS0117 and CS1061
                StatusPayment = o.StatusPayment ?? 0 // Fix for CS0117 and CS1061
            }).ToList();
        }

        public OrderDTO GetOrderById(int orderId)
        {
            var order = _orderRepository.GetOrderByIdAsync(orderId).Result;
            if (order == null)
            {
                return new OrderDTO(); // Return a new instance to avoid null reference
            }

            return new OrderDTO
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Note = order.Note,
                TotalPrice = order.TotalPrice ?? 0,
                StatusOrder = order.StatusOrder ?? 0,
                StatusPayment = order.StatusPayment ?? 0,
                CustomerId = order.CustomerId ?? 0
            };
        }

        // Cập nhật trạng thái đơn hàng
        public void UpdateOrder(OrderDTO orderDTO)
        {
            var order = _orderRepository.GetOrderByIdAsync(orderDTO.OrderId).Result;
            if (order != null)
            {
                order.StatusOrder = orderDTO.StatusOrder;
                _orderRepository.UpdateOrderStatusAsync(order.OrderId, order.StatusOrder ?? 0).Wait();
            }
        }
    }
}
