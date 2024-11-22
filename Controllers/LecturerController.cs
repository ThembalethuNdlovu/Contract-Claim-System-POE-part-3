using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PROG6212_Part1.Models;

namespace PROG6212_Part1.Controllers
{
    public class LecturerController : Controller
    {
        // Shared list of claims accessible to all users
        public static List<Claim> claims = new List<Claim>();

        // Define acceptable file types and a maximum upload size of 5MB
        private static readonly string[] AllowedFileExtensions = { ".pdf", ".docx", ".xlsx" };
        private const long MaxFileSize = 5 * 1024 * 1024; // Maximum size for uploaded files

        // Display the Submit and Track Claim page with the current list of claims
        public IActionResult SubmitAndTrackClaim()
        {
            return View(claims); // Send the claims data to the view for display
        }

        // Handle form submissions for claims with attached files
        [HttpPost]
        public IActionResult SubmitClaim(string lecturer, int hoursWorked, decimal hourlyRate, string notes, IFormFile document)
        {
            // Verify that the required fields are valid
            if (hoursWorked <= 0 || hourlyRate <= 0 || document == null)
            {
                ViewBag.ErrorMessage = "Invalid data or missing document.";
                return View("SubmitAndTrackClaim", claims);
            }

            // Check file type and ensure it matches allowed formats
            var fileExtension = Path.GetExtension(document.FileName).ToLower();
            if (!AllowedFileExtensions.Contains(fileExtension))
            {
                ViewBag.ErrorMessage = "Unsupported file type. Use .pdf, .docx, or .xlsx only.";
                return View("SubmitAndTrackClaim", claims);
            }

            // Enforce file size limits
            if (document.Length > MaxFileSize)
            {
                ViewBag.ErrorMessage = "File exceeds the maximum allowed size of 5MB.";
                return View("SubmitAndTrackClaim", claims);
            }

            // Generate a unique ID for the claim
            int newClaimId = claims.Count + 1;

            // Set up the folder to store uploaded files
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder); // Create the folder if it doesn't exist
            }

            // Save the file with a unique name
            string uniqueFileName = $"{newClaimId}_{Path.GetFileName(document.FileName)}";
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                document.CopyTo(fileStream); // Write the file to the server
            }

            // Add the new claim to the shared list
            claims.Add(new Claim
            {
                ClaimId = newClaimId,
                Lecturer = lecturer,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                Notes = notes,
                DocumentPath = $"/uploads/{uniqueFileName}", // Store the relative file path
                Status = "Pending" // Default claim status
            });

            // Return to the Submit and Track Claim page
            return RedirectToAction("SubmitAndTrackClaim");
        }
    }
}
