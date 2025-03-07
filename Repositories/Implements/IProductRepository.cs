using FoodOrderSystem.Models;

namespace FoodOrderSystem.Repositories.Implements
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<int> InsertProductAsync(Product product);
        Task<bool> InsertProductCategoryAsync(int categoryId, int productId);
        Task<bool> DeleteProductCategoriesAsync(int productId);
        Task<bool> UpdateProductInfoAsync(int productId, string name, int price, string description, DateOnly releaseDate, string author, int quantity);
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> ChangeProductStatusAsync(int productId, int status);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<List<Product>> GetAllProductsWithPagingAsync(int page, int pageSize);
        Task<int> GetTotalProductsAsync();
        Task<List<Product>> SearchProductsAsync(int priceMin, int priceMax, List<int> categoryIds, int page, int pageSize, string name);
    }
}
