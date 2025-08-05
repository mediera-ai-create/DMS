using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Model { get; set; }
        public required string Make { get; set; }
        public required string VIN { get; set; }
        public required string Status { get; set; }
        public int DealerId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsReorderRequired { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Default to current time

    }

}
