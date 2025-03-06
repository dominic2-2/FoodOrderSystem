using FoodOrderSystem.DTOs;
using FoodOrderSystem.Repositories.Implements;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<StaffDTO?> GetStaffByAccountIdAsync(int accountId)
        {
            var staff = await _staffRepository.GetStaffByAccountIdAsync(accountId);
            if (staff == null) return null;

            return new StaffDTO
            {
                StaffId = staff.StaffId,
                AccountId = staff.AccountId ?? 0,
                Role = staff.Role ?? 0
            };
        }

        public async Task<bool> InsertStaffAsync(int accountId, int role)
        {
            return await _staffRepository.InsertStaffAsync(accountId, role);
        }
    
    }
}
