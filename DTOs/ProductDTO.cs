namespace FoodOrderSystem.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public int StatusPro { get; set; }
    }
}
