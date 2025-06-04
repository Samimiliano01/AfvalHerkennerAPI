using Microsoft.AspNetCore.Authorization;  // For [Authorize] attribute to restrict access
using Microsoft.AspNetCore.Identity;       // For user management and roles (UserManager)
using Microsoft.AspNetCore.Mvc;             // For controller base and HTTP action results

namespace Sensoring_API.Controllers;

[Authorize(Roles = "Admin")]                // Only users in the Admin role can access this controller
[ApiController]                            // Enables API-specific behaviors like automatic model validation
[Route("admin")]                          // Route prefix: all endpoints start with /admin
public class AdminController(UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpPost]                            // Handles HTTP POST requests to promote a user to Admin role
    public async Task<IActionResult> Create([FromBody] string email)
    {
        var user = await userManager.FindByEmailAsync(email);  // Find user by email

        if (user == null)                // If user not found
        {
            return NotFound($"No user found with email: {email}");  // Return 404 Not Found
        }

        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");  // Check if user already Admin

        if (isAdmin)              // If user is already an admin
        {
            return BadRequest("User is already an admin.");     // Return 400 Bad Request
        }
        
        var result = await userManager.AddToRoleAsync(user, "Admin");  // Try to add Admin role

        if (result.Succeeded)            // If adding role succeeded
        {
            return Ok($"User {email} promoted to Admin.");  // Return success message
        } 
        
        // If adding role failed
        return BadRequest("Failed to assign admin role.");  // Return failure message
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] string email)
    {
        // Find the user by email
        var user = await userManager.FindByEmailAsync(email);

        // Return 404 if the user does not exist
        if (user == null)
        {
            return NotFound($"No user found with email: {email}");
        }
    
        // Check if the user is currently in the Admin role
        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
    
        // Return 400 if the user is not an admin
        if (!isAdmin)
        {
            return BadRequest("User is not an admin.");
        }
    
        // Attempt to remove the Admin role
        var result = await userManager.RemoveFromRoleAsync(user, "Admin");

        // Return 200 if successful
        if (result.Succeeded)
        {
            return Ok($"User {email} downgraded to user.");
        }
    
        // Return 400 if removal failed
        return BadRequest("Failed to remove admin role.");
    }
}