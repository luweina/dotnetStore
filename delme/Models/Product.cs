using System;
using System.Collections.Generic;

#nullable disable

namespace delme.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductInvoices = new HashSet<ProductInvoice>();
            ProductPurchaseOrders = new HashSet<ProductPurchaseOrder>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Mfg { get; set; }
        public string Vendor { get; set; }
        public decimal? Price { get; set; }

        public virtual Manufacturer MfgNavigation { get; set; }
        public virtual Supplier VendorNavigation { get; set; }
        public virtual ICollection<ProductInvoice> ProductInvoices { get; set; }
        public virtual ICollection<ProductPurchaseOrder> ProductPurchaseOrders { get; set; }
    }
}
