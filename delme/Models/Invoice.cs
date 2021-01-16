using System;
using System.Collections.Generic;

#nullable disable

namespace delme.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            ProductInvoices = new HashSet<ProductInvoice>();
        }

        public int InvoiceNum { get; set; }
        public string Branch { get; set; }

        public virtual Store BranchNavigation { get; set; }
        public virtual ICollection<ProductInvoice> ProductInvoices { get; set; }
    }
}
