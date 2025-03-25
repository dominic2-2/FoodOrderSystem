using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem.Models;
using FoodOrderSystem.Data;

namespace FoodOrderSystem.Controllers.Common
{
    public class FeedbackController : Controller
    {
        private readonly FoodOrderSystemContext _context;

        public FeedbackController(FoodOrderSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var feedbacks = await _context.ProductFeedbacks
                .Include(f => f.Product)
                .Include(f => f.Customer)
                .ToListAsync();

            return View(feedbacks);
        }
    }
}
