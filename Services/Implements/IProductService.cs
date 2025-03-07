using FoodOrderSystem.DTOs;

namespace FoodOrderSystem.Services.Implements
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<int> InsertProductAsync(ProductDTO productDTO);
        Task<bool> InsertProductCategoryAsync(int categoryId, int productId);
        Task<bool> DeleteProductCategoriesAsync(int productId);
        Task<bool> UpdateProductInfoAsync(int productId, string name, int price, string description, DateOnly releaseDate, string author, int quantity);
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> UpdateProductAsync(ProductDTO productDTO);
        Task<bool> ChangeProductStatusAsync(int productId, int status);
        Task<ProductDTO?> GetProductByIdAsync(int productId);
        Task<List<ProductDTO>> GetAllProductsWithPagingAsync(int page, int pageSize);
        Task<int> GetTotalProductsAsync();
        Task<List<ProductDTO>> SearchProductsAsync(int priceMin, int priceMax, List<int> categoryIds, int page, int pageSize, string name);
    }
}
