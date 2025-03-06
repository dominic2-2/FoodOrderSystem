using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public int? StatusAcc { get; set; }

    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    public int? Gender { get; set; }

    public string? Token { get; set; }

    public int? Role { get; set; }

    public TimeOnly? UpdateTime { get; set; }

    public TimeOnly? CreateTime { get; set; }

    public string? UpdateBy { get; set; }

    public string? CreateBy { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
