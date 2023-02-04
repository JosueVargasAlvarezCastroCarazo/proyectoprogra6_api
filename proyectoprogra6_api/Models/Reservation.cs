using System;
using System.Collections.Generic;

namespace proyectoprogra6_api.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public string Notes { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
