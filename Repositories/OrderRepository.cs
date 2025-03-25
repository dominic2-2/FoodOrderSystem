using FoodOrderSystem.Data;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodOrderSystemContext _context;

        public OrderRepository(FoodOrderSystemContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .Include(o => o.Shipping)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .Include(o => o.Shipping)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderDetails)
                .ToListAsync();
        }

        public async Task<int> InsertOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, int statusOrder)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.StatusOrder = statusOrder;
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePaymentStatusAsync(int orderId, int statusPayment)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.StatusPayment = statusPayment;
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Customer).ToList();
        }

        // Lưu thay đổi vào cơ sở dữ liệu
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }

}
