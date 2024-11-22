#nullable enable

namespace ContractClaimSystem_POE_Part3.Models
{
    // Represents a lecturer's claim for hours worked
    public class Claim
    {
        // Unique identifier for the claim
        public int ClaimId { get; set; }

        // Name of the lecturer submitting the claim (optional)
        public string? Lecturer { get; set; }

        // Total number of hours the lecturer has worked
        public int HoursWorked { get; set; }

        // Hourly pay rate for the lecturer
        public decimal HourlyRate { get; set; }

        // Additional details or remarks for the claim (optional)
        public string? Notes { get; set; }

        // Current status of the claim, default is "Pending"
        public string Status { get; set; } = "Pending";

        // File path for any uploaded documents supporting the claim (optional)
        public string? DocumentPath { get; set; }
    }
}
