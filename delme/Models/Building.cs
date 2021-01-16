using System;
using System.Collections.Generic;

#nullable disable

namespace delme.Models
{
    public partial class Building
    {
        public Building()
        {
            Stores = new HashSet<Store>();
        }

        public string BuildingName { get; set; }
        public int UnitNum { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
