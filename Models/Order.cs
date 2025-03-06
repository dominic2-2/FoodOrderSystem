using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Note { get; set; }

    public int? TotalPrice { get; set; }

    public int? StatusOrder { get; set; }

    public int? StatusPayment { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Shipping? Shipping { get; set; }
}
