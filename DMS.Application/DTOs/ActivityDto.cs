using System;

namespace DMS.Application.DTOs
{
    public class ActivityDto
    {
        public int DealerId { get; set; }
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public required string AdditionalFiles { get; set; }
        public required string Remarks { get; set; }
        public required string PhotoPath { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public required string UserId { get; set; }
    }
}
