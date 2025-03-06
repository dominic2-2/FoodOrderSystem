using FoodOrderSystem.Models;

namespace FoodOrderSystem.Repositories.Implements
{
    public interface IStaffRepository
    {
        Task<Staff?> GetStaffByAccountIdAsync(int accountId);
        Task<List<Staff>> GetStaffAdminsAsync();
        Task<bool> InsertStaffAsync(int accountId, int role);
    }
}
