using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class ProductFeedback
{
    public int FeedbackId { get; set; }

    public int? ProductId { get; set; }

    public int? Star { get; set; }

    public string? Comment { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
