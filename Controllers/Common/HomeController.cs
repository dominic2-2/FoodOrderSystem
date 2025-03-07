using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodOrderSystem.Services;
using FoodOrderSystem.DTOs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodOrderSystem.Models;
using FoodOrderSystem.Services.Implements;

namespace FoodOrderSystem.Controllers.Common
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private const int PAGE_SIZE = 3;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                List<ProductDTO> productList = await _productService.GetAllProductsWithPagingAsync(page, PAGE_SIZE);
                int totalProducts = await _productService.GetTotalProductsAsync();
                int totalPage = (totalProducts + PAGE_SIZE - 1) / PAGE_SIZE;
                List<CategoryDTO> categoryList = await _categoryService.GetAllCategoriesAsync();

                ViewData["productList"] = productList;
                ViewData["categoryList"] = categoryList;
                ViewData["page"] = page;
                ViewData["totalPage"] = totalPage;

                return View("ProductListPage");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in HomeController.Index: {ex.Message}");
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return View("ProductListPage");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchProducts(string name, int priceMin = 0, int priceMax = 999999, int[] categoryIds = null, int page = 1)
        {
            try
            {
                categoryIds ??= new int[0];
                List<ProductDTO> productList = await _productService.SearchProductsAsync(priceMin, priceMax, new List<int>(categoryIds), page, PAGE_SIZE, name);
                int totalProducts = await _productService.GetTotalProductsAsync();
                int totalPage = (totalProducts + PAGE_SIZE - 1) / PAGE_SIZE;
                List<CategoryDTO> categoryList = await _categoryService.GetAllCategoriesAsync();

                ViewData["productList"] = productList;
                ViewData["categoryList"] = categoryList;
                ViewData["page"] = page;
                ViewData["totalPage"] = totalPage;
                ViewData["searchName"] = name;
                ViewData["searchPriceMin"] = priceMin;
                ViewData["searchPriceMax"] = priceMax;
                ViewData["searchCategoryIds"] = categoryIds;

                return View("ProductListPage");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in HomeController.SearchProducts: {ex.Message}");
                ViewData["ERROR_MESSAGE"] = $"Error: {ex.Message}";
                return View("ProductListPage");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
