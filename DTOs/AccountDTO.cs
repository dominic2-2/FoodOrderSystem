namespace FoodOrderSystem.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string? Email { get; set; }
        public string? Fullname { get; set; }
        public string? Phone { get; set; }
        public int StatusAcc { get; set; }
        public string? Address { get; set; }
        public DateOnly Dob { get; set; }
        public int Gender { get; set; }
        public int Role { get; set; }
    }
}
