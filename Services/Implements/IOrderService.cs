using FoodOrderSystem.DTOs;

namespace FoodOrderSystem.Services.Implements
{
    public interface IOrderService
    {
        Task<OrderDTO?> GetOrderByIdAsync(int orderId);
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<List<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId);
        Task<int> InsertOrderAsync(OrderDTO orderDTO);
        Task<bool> UpdateOrderStatusAsync(int orderId, int statusOrder);
        Task<bool> UpdatePaymentStatusAsync(int orderId, int statusPayment);
        Task<bool> DeleteOrderAsync(int orderId);
        List<OrderDTO> GetAllOrders();
        OrderDTO GetOrderById(int orderId);
        void UpdateOrder(OrderDTO orderDTO);
    }
}
