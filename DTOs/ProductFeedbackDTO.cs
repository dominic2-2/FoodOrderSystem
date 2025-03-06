namespace FoodOrderSystem.DTOs
{
    public class ProductFeedbackDTO
    {
        public int FeedbackId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateOnly FeedbackDate { get; set; }
    }
}
