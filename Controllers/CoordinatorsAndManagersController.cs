using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PROG6212_Part1.Models;

namespace PROG6212_Part1.Controllers
{
    public class CoordinatorsAndManagersController : Controller
    {
        // Access the shared list of claims from LecturerController
        private static List<Claim> claims = LecturerController.claims;

        // Display the page for reviewing claims
        public IActionResult ReviewClaims()
        {
            return View(claims); // Pass claims data to the view for coordinators/managers
        }

        // Approve a claim by changing its status
        [HttpPost]
        public IActionResult ApproveClaim(int claimId)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                claim.Status = "Approved"; // Mark claim as approved
            }
            return RedirectToAction("ReviewClaims"); // Refresh the review page
        }

        // Reject a claim by changing its status
        [HttpPost]
        public IActionResult RejectClaim(int claimId)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                claim.Status = "Rejected"; // Mark claim as rejected
            }
            return RedirectToAction("ReviewClaims"); // Refresh the review page
        }
    }
}
