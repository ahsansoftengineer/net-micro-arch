// using Microsoft.AspNetCore.Authorization;

// namespace SBA.Auth.Controllers;
// public partial class ProfileController
// {
//   [HttpGet]
//   public async Task<IActionResult> GetCurrentUser()
//   {
//     // Get info about the currently logged-in user
//     var user = await _userManager.GetUserAsync(User);
//     if (user == null) return Unauthorized();

//     return Ok(new {
//         user.Id,
//         user.UserName,
//         user?.Email,
//         Roles = await _userManager.GetRolesAsync(user)
//     });
//   }

//   [HttpGet]
//   public async Task<IActionResult> GetUserSessions()
//   {
//     // List all active sessions for the user
//     var userId = _userManager.GetUserId(User);
//     var sessions = await _uowPro.UserSessions
//         .Where(s => s.UserId == userId && s.LogoutTime == null)
//         .ToListAsync();

//     return Ok(sessions);
//   }
//   [HttpDelete("{Id}")]
//   public async Task<IActionResult> RevokeSession(Guid sessionId)
//   {
//     // End a specific session (logout remotely)
//     var userId = _userManager.GetUserId(User);
//     var session = await _dbContext.UserSessions
//         .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);

//     if (session == null) return NotFound();

//     session.LogoutTime = DateTime.UtcNow;
//     await _dbContext.SaveChangesAsync();

//     // Optional: invalidate security stamp or JWT
//     return Ok(new { message = "Session revoked" });
//   }

// }