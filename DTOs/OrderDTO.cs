namespace FoodOrderSystem.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateOnly OrderDate { get; set; }
        public string? Note { get; set; }
        public int TotalPrice { get; set; }
        public int StatusOrder { get; set; }
        public int StatusPayment { get; set; }
        public int CustomerId { get; set; }
    }
}
