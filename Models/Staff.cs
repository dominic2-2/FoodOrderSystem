using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public int? AccountId { get; set; }

    public int? Role { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
