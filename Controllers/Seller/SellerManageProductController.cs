using FoodOrderSystem.Services;
using FoodOrderSystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Controllers.Seller
{
    public class SellerManageProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public SellerManageProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        private bool IsAuthorized()
        {
            return HttpContext.Session.GetString("ROLE") == "Seller";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                List<ProductDTO> productList = await _productService.GetAllProductsAsync();
                List<CategoryDTO> categoryList = await _categoryService.GetAllCategoriesAsync();

                ViewData["productList"] = productList;
                ViewData["categoryList"] = categoryList;
                return View("ManageProductsPage");
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return View("ManageProductsPage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(string name, int price, string description, DateOnly releaseDate, string author, int quantity, int[] categoryIds, IFormFile imageFile)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                string imagePath = await SaveImageAsync(imageFile);

                var newProduct = new ProductDTO
                {
                    ProductName = name,
                    Price = price,
                    Description = description,
                    ReleaseDate = releaseDate,
                    Author = author,
                    Quantity = quantity,
                    Img = imagePath,
                    StatusPro = 1
                };

                int productId = await _productService.InsertProductAsync(newProduct);
                if (productId > 0)
                {
                    var validCategories = await _categoryService.GetAllCategoriesAsync();
                    var existingCategoryIds = validCategories.Select(c => c.CategoryId).ToHashSet();

                    foreach (var categoryId in categoryIds)
                    {
                        if (existingCategoryIds.Contains(categoryId))
                        {
                            await _productService.InsertProductCategoryAsync(categoryId, productId);
                        }
                    }

                    ViewData["MSG_SUCCESS"] = "Product created successfully!";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Failed to create product!";
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return await Index();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int productId, string name, int price, string description, DateOnly releaseDate, string author, int quantity, int[] categoryIds, IFormFile imageFile)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                string imagePath = await SaveImageAsync(imageFile);

                var updatedProduct = new ProductDTO
                {
                    ProductId = productId,
                    ProductName = name,
                    Price = price,
                    Description = description,
                    ReleaseDate = releaseDate,
                    Author = author,
                    Quantity = quantity,
                    Img = imagePath
                };

                bool isUpdated = await _productService.UpdateProductAsync(updatedProduct);

                if (isUpdated)
                {
                    await _productService.DeleteProductCategoriesAsync(productId);

                    var validCategories = await _categoryService.GetAllCategoriesAsync();
                    var existingCategoryIds = validCategories.Select(c => c.CategoryId).ToHashSet();

                    foreach (var categoryId in categoryIds)
                    {
                        if (existingCategoryIds.Contains(categoryId))
                        {
                            await _productService.InsertProductCategoryAsync(categoryId, productId);
                        }
                    }

                    ViewData["MSG_SUCCESS"] = "Product updated successfully!";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Failed to update product!";
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return await Index();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                bool isDeleted = await _productService.DeleteProductAsync(productId);

                if (isDeleted)
                {
                    ViewData["MSG_SUCCESS"] = "Product deleted successfully!";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Failed to delete product!";
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return await Index();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProductStatus(int productId)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                ProductDTO? product = await _productService.GetProductByIdAsync(productId);
                if (product == null)
                {
                    ViewData["ERROR_MESSAGE"] = "Product not found!";
                    return await Index();
                }

                product.StatusPro = product.StatusPro == 1 ? 0 : 1;
                bool updated = await _productService.UpdateProductAsync(product);

                if (updated)
                {
                    ViewData["MSG_SUCCESS"] = product.StatusPro == 1 ? "Product activated successfully!" : "Product deactivated successfully!";
                }
                else
                {
                    ViewData["ERROR_MESSAGE"] = "Failed to update product status!";
                }

                return await Index();
            }
            catch (Exception ex)
            {
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return await Index();
            }
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return "/img/default.jpg";

            try
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);

                // Nếu đường dẫn quá dài, cắt ngắn lại để tránh lỗi
                if (uniqueFileName.Length > 100)
                {
                    uniqueFileName = uniqueFileName.Substring(0, 100);
                }

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return "/img/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving image: {ex.Message}");
                return "/img/default.jpg";
            }
        }
    }
}
