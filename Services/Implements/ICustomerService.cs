using FoodOrderSystem.DTOs;

namespace FoodOrderSystem.Services.Implements
{
    public interface ICustomerService
    {
        Task<CustomerDTO?> GetCustomerByIdAsync(int customerId);
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<bool> InsertCustomerAsync(int accountId, int point);
    }
}
