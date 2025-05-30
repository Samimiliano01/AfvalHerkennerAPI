using Microsoft.AspNetCore.Identity;

namespace Sensoring_API.Services;

public class MockEmailSender : IEmailSender<IdentityUser>
{
    
    public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        Console.WriteLine($"[Dummy] Sending confirmation link to {email}: {confirmationLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        Console.WriteLine($"[Dummy] Sending password reset link to {email}: {resetLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        Console.WriteLine($"[Dummy] Sending password reset code to {email}: {resetCode}");
        return Task.CompletedTask;
    }
}