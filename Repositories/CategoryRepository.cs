using FoodOrderSystem.Data;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FoodOrderSystemContext _context;

        public CategoryRepository(FoodOrderSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
