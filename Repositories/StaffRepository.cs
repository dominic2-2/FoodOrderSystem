using FoodOrderSystem.Data;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly FoodOrderSystemContext _context;

        public StaffRepository(FoodOrderSystemContext context)
        {
            _context = context;
        }

        public async Task<Staff?> GetStaffByAccountIdAsync(int accountId)
        {
            return await _context.Staff.FirstOrDefaultAsync(s => s.AccountId == accountId);
        }

        public async Task<List<Staff>> GetStaffAdminsAsync()
        {
            return await _context.Staff.Where(s => s.Role == 0).ToListAsync();
        }

        public async Task<bool> InsertStaffAsync(int accountId, int role)
        {
            var staff = new Staff
            {
                AccountId = accountId,
                Role = role
            };

            await _context.Staff.AddAsync(staff);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
