using System;
using System.Collections.Generic;

#nullable disable

namespace delme.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            ProductPurchaseOrders = new HashSet<ProductPurchaseOrder>();
        }

        public int PoNum { get; set; }
        public string Branch { get; set; }

        public virtual Store BranchNavigation { get; set; }
        public virtual ICollection<ProductPurchaseOrder> ProductPurchaseOrders { get; set; }
    }
}
