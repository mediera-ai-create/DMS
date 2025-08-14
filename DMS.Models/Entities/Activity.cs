using System;

namespace DMS.Models.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        public int DealerId { get; set; }
        public required string DealerName { get; set; }

        public int RequestId { get; set; }
        public required string RequestName { get; set; }
        public required string UserId { get; set; } 
        public int ItemId { get; set; }
        public required string ItemName { get; set; }

        public required string AdditionalFiles { get; set; } // Comma-separated file paths or JSON array

        public required string Remarks { get; set; } // Multiline in UI

        public required string PhotoPath { get; set; } // Path to uploaded photo

        public DateTime? NextFollowUpDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Dealer? Dealer { get; set; }
        public Request? Request { get; set; }
        public Item? Item { get; set; }
    }
}
