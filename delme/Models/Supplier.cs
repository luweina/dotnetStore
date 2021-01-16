using System;
using System.Collections.Generic;

#nullable disable

namespace delme.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public string Vendor { get; set; }
        public string SupplierEmail { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
