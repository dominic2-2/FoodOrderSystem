using System;
using System.Collections.Generic;

namespace FoodOrderSystem.Models;

public partial class Shipping
{
    public int ShipId { get; set; }

    public string? Address { get; set; }

    public int? StatusShip { get; set; }

    public int? StatusPayment { get; set; }

    public int? StaffId { get; set; }

    public virtual Order Ship { get; set; } = null!;

    public virtual Staff? Staff { get; set; }
}
