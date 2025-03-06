namespace FoodOrderSystem.DTOs
{
    public class ShippingDTO
    {
        public int ShippingId { get; set; }
        public int OrderId { get; set; }
        public int StaffId { get; set; }
        public string Status { get; set; }
        public DateOnly ShippingDate { get; set; }
    }
}
