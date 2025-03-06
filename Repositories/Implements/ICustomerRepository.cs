using FoodOrderSystem.Models;

namespace FoodOrderSystem.Repositories.Implements
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<bool> InsertCustomerAsync(int accountId, int point);
    }
}
