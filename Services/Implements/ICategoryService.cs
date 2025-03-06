using FoodOrderSystem.DTOs;

namespace FoodOrderSystem.Services.Implements
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
    }
}
