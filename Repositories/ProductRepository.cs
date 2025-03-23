using FoodOrderSystem.Data;
using FoodOrderSystem.Models;
using FoodOrderSystem.Repositories.Implements;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FoodOrderSystemContext _context;

        public ProductRepository(FoodOrderSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<int> InsertProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task<bool> InsertProductCategoryAsync(int categoryId, int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var category = await _context.Categories.FindAsync(categoryId);

            if (product == null || category == null) return false;

            product.Categories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductCategoriesAsync(int productId)
        {
            var product = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null) return false;

            product.Categories.Clear();
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductInfoAsync(int productId, string name, int price, string description, DateOnly releaseDate, string author, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            product.ProductName = name;
            product.Price = price;
            product.Description = description;
            product.ReleaseDate = releaseDate;
            product.Author = author;
            product.Quantity = quantity;
            product.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);

            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            product.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeProductStatusAsync(int productId, int status)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            product.StatusPro = status;
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<Product>> GetAllProductsWithPagingAsync(int page, int pageSize)
        {
            return await _context.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<Product>> SearchProductsAsync(int priceMin, int priceMax, List<int> categoryIds, int page, int pageSize, string name)
        {
            var query = _context.Products.AsQueryable();

            if (priceMin >= 0 && priceMax >= 0)
                query = query.Where(p => p.Price >= priceMin && p.Price <= priceMax);
            else if (priceMin >= 0)
                query = query.Where(p => p.Price >= priceMin);
            else if (priceMax >= 0)
                query = query.Where(p => p.Price <= priceMax);

            if (categoryIds != null && categoryIds.Count > 0)
                query = query.Where(p => p.Categories.Any(c => categoryIds.Contains(c.CategoryId)));

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => (p.ProductName != null && p.ProductName.Contains(name)) || (p.Author != null && p.Author.Contains(name)));

            return await query
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Product>> GetBestSellerProducts(string StatusPro)
        {
            return await _context.Products
                .Where(p => p.StatusPro == int.Parse(StatusPro))
                .OrderByDescending(p => p.Quantity)
                .Take(10)
                .ToListAsync();
        }
    }
}
