using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TblMember
    {
        public TblMember()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
