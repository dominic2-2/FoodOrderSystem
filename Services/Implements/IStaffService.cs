using FoodOrderSystem.DTOs;

namespace FoodOrderSystem.Services.Implements
{
    public interface IStaffService
    {
        Task<StaffDTO?> GetStaffByAccountIdAsync(int accountId);
        Task<bool> InsertStaffAsync(int accountId, int role);
    }
}
