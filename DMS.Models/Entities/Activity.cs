using System;

namespace DMS.Models.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        public int DealerId { get; set; }
        public required Dealer Dealer { get; set; }

        public int RequestId { get; set; }
        public required Request Request { get; set; }

        public int ItemId { get; set; }
        public required Item Item { get; set; }

        public required string AdditionalFiles { get; set; } // Comma-separated file paths or JSON array

        public required string Remarks { get; set; } // Multiline in UI

        public required string PhotoPath { get; set; } // Path to uploaded photo

        public DateTime? NextFollowUpDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
