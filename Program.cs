using Microsoft.EntityFrameworkCore;

// Entry point for the application
var builder = WebApplication.CreateBuilder(args);

// Register services required by the application
builder.Services.AddControllersWithViews(); // Add support for controllers and views

var app = builder.Build();

// Define behavior for handling HTTP requests and responses
if (!app.Environment.IsDevelopment())
{
    // Use a custom error handler for non-development environments
    app.UseExceptionHandler("/Home/Error");
    
    // Enforce HTTPS for production with a default 30-day HSTS policy
    app.UseHsts();
}

// Redirect HTTP traffic to HTTPS
app.UseHttpsRedirection();

// Enable serving static files (e.g., CSS, JS, images)
app.UseStaticFiles();

// Configure request routing
app.UseRouting();

// Implement authorization middleware for secure access
app.UseAuthorization();

// Map URL patterns to controllers and actions
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lecturer}/{action=SubmitAndTrackClaim}/{id?}");

// Start the application
app.Run();
