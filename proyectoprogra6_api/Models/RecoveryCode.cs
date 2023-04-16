using System;
using System.Collections.Generic;

namespace proyectoprogra6_api.Models
{
    public partial class RecoveryCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}
