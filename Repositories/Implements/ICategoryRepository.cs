using FoodOrderSystem.Models;

namespace FoodOrderSystem.Repositories.Implements
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
