using System;
using System.Collections.Generic;

#nullable disable

namespace delme.Models
{
    public partial class Store
    {
        public Store()
        {
            Employees = new HashSet<Employee>();
            Invoices = new HashSet<Invoice>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public string Branch { get; set; }
        public string Region { get; set; }
        public string BuildingName { get; set; }
        public int? UnitNum { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
