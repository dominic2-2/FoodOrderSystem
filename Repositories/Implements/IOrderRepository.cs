using FoodOrderSystem.Models;

namespace FoodOrderSystem.Repositories.Implements
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<int> InsertOrderAsync(Order order);
        Task<bool> UpdateOrderStatusAsync(int orderId, int statusOrder);
        Task<bool> UpdatePaymentStatusAsync(int orderId, int statusPayment);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
