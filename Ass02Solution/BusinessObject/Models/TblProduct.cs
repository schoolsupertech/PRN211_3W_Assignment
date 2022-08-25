using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TblProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public decimal UnitPrice { get; set; }
    }
}
