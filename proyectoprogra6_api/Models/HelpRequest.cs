using System;
using System.Collections.Generic;

namespace proyectoprogra6_api.Models
{
    public partial class HelpRequest
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
