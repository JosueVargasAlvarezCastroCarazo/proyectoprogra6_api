using System;
using System.Collections.Generic;

namespace proyectoprogra6_api.Models
{
    public partial class User
    {
        public User()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string LoginPassword { get; set; } = null!;
        public bool? IsAdmin { get; set; }
        public string Identification { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
