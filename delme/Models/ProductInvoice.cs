using System;
using System.Collections.Generic;

#nullable disable

namespace delme.Models
{
    public partial class ProductInvoice
    {
        public int ProductId { get; set; }
        public int InvoiceNum { get; set; }
        public int? Quantity { get; set; }

        public virtual Invoice InvoiceNumNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
