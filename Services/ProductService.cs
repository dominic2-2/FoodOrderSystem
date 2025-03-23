using FoodOrderSystem.DTOs;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName ?? string.Empty,
                Price = p.Price ?? 0,
                Img = p.Img ?? string.Empty,
                Description = p.Description ?? string.Empty,
                ReleaseDate = p.ReleaseDate ?? DateOnly.FromDateTime(DateTime.Now),
                Author = p.Author ?? string.Empty,
                Quantity = p.Quantity ?? 0,
                StatusPro = p.StatusPro ?? 0
            }).ToList();
        }

        public async Task<int> InsertProductAsync(ProductDTO productDTO)
        {
            var product = new Product
            {
                ProductName = productDTO.ProductName,
                Price = productDTO.Price,
                Img = productDTO.Img,
                Description = productDTO.Description,
                ReleaseDate = productDTO.ReleaseDate,
                Author = productDTO.Author,
                Quantity = productDTO.Quantity,
                StatusPro = productDTO.StatusPro
            };

            return await _productRepository.InsertProductAsync(product);
        }

        public async Task<bool> InsertProductCategoryAsync(int categoryId, int productId)
        {
            return await _productRepository.InsertProductCategoryAsync(categoryId, productId);
        }

        public async Task<bool> DeleteProductCategoriesAsync(int productId)
        {
            return await _productRepository.DeleteProductCategoriesAsync(productId);
        }

        public async Task<bool> UpdateProductInfoAsync(int productId, string name, int price, string description, DateOnly releaseDate, string author, int quantity)
        {
            return await _productRepository.UpdateProductInfoAsync(productId, name, price, description, releaseDate, author, quantity);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await _productRepository.DeleteProductAsync(productId);
        }

        public async Task<bool> UpdateProductAsync(ProductDTO productDTO)
        {
            var product = new Product
            {
                ProductId = productDTO.ProductId,
                ProductName = productDTO.ProductName,
                Price = productDTO.Price,
                Img = productDTO.Img,
                Description = productDTO.Description,
                ReleaseDate = productDTO.ReleaseDate,
                Author = productDTO.Author,
                Quantity = productDTO.Quantity,
                StatusPro = productDTO.StatusPro
            };

            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> ChangeProductStatusAsync(int productId, int status)
        {
            return await _productRepository.ChangeProductStatusAsync(productId, status);
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) return null;

            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName ?? string.Empty,
                Price = product.Price ?? 0,
                Img = product.Img ?? string.Empty,
                Description = product.Description ?? string.Empty,
                ReleaseDate = product.ReleaseDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Author = product.Author ?? string.Empty,
                Quantity = product.Quantity ?? 0,
                StatusPro = product.StatusPro ?? 0
            };
        }

        public async Task<List<ProductDTO>> GetAllProductsWithPagingAsync(int page, int pageSize)
        {
            var products = await _productRepository.GetAllProductsWithPagingAsync(page, pageSize);
            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName ?? string.Empty,
                Price = p.Price ?? 0,
                Img = p.Img ?? string.Empty,
                Description = p.Description ?? string.Empty,
                ReleaseDate = p.ReleaseDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Author = p.Author ?? string.Empty,
                Quantity = p.Quantity ?? 0,
                StatusPro = p.StatusPro ?? 0
            }).ToList();
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _productRepository.GetTotalProductsAsync();
        }

        public async Task<List<ProductDTO>> SearchProductsAsync(int priceMin, int priceMax, List<int> categoryIds, int page, int pageSize, string name)
        {
            var products = await _productRepository.SearchProductsAsync(priceMin, priceMax, categoryIds, page, pageSize, name);
            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName ?? string.Empty,
                Price = p.Price ?? 0,
                Img = p.Img ?? string.Empty,
                Description = p.Description ?? string.Empty,
                ReleaseDate = p.ReleaseDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Author = p.Author ?? string.Empty,
                Quantity = p.Quantity ?? 0,
                StatusPro = p.StatusPro ?? 0
            }).ToList();
        }

        public async Task<List<ProductDTO>> GetBestSellerProducts(string StatusPro)
        {
            var products = await _productRepository.GetBestSellerProducts(StatusPro);
            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName ?? string.Empty,
                Price = p.Price ?? 0,
                Img = p.Img ?? string.Empty,
                Description = p.Description ?? string.Empty,
                ReleaseDate = p.ReleaseDate ?? DateOnly.FromDateTime(System.DateTime.Now),
                Author = p.Author ?? string.Empty,
                Quantity = p.Quantity ?? 0,
                StatusPro = p.StatusPro ?? 0
            }).ToList();
        }
    }
}
