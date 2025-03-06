using FoodOrderSystem.Data;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderSystem.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FoodOrderSystemContext _context;

        public CustomerRepository(FoodOrderSystemContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Account) // Load Account nếu cần
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Include(c => c.Account)
                .ToListAsync();
        }

        public async Task<bool> InsertCustomerAsync(int accountId, int point)
        {
            var customer = new Customer
            {
                AccountId = accountId,
                Point = point
            };

            await _context.Customers.AddAsync(customer);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
