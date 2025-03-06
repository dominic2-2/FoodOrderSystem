using FoodOrderSystem.DTOs;
using FoodOrderSystem.Repositories.Implements;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null) return null;

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                AccountId = customer.AccountId ?? 0,
                Point = customer.Point ?? 0
            };
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                AccountId = c.AccountId ?? 0,
                Point = c.Point ?? 0
            }).ToList();
        }

        public async Task<bool> InsertCustomerAsync(int accountId, int point)
        {
            return await _customerRepository.InsertCustomerAsync(accountId, point);
        }
    }
}
