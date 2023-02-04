using System;
using System.Collections.Generic;

namespace proyectoprogra6_api.Models
{
    public partial class Item
    {
        public Item()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemDescription { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
