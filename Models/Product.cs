using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? Price { get; set; }

    public string? Img { get; set; }

    public string? Description { get; set; }

    public string? ProductName { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? Author { get; set; }

    public int? Quantity { get; set; }

    public int? StatusPro { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public TimeOnly? UpdateTime { get; set; }

    public TimeOnly? CreateTime { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductFeedback> ProductFeedbacks { get; set; } = new List<ProductFeedback>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
