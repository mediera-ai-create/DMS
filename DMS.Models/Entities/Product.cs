using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Model { get; set; }
        public required string Make { get; set; }
        public required string VIN { get; set; }
        public required string Status { get; set; }  // Inbound, Outbound, InStock, Sold
        public int DealerId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsReorderRequired { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ReorderLevel { get; set; }  // property to track threshold

    }


}
