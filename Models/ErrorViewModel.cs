namespace PROG6212_Part1.Models
{
    // Represents information about errors in the application
    public class ErrorViewModel
    {
        // Unique identifier for the request that triggered the error (optional)
        public string? RequestId { get; set; }

        // Indicates if a valid RequestId is present
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
