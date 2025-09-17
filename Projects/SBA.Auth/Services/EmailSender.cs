using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace SBA.Auth.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly Option_EmailSettings _email;

    public SmtpEmailSender(IOptions<Option_EmailSettings> emailSettings)
    {
        _email = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailz = new MimeMessage();
        emailz.From.Add(MailboxAddress.Parse(_email.From));
        emailz.To.Add(MailboxAddress.Parse(email));
        emailz.Subject = subject;
        emailz.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_email.SmtpServer, _email.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_email.Username, _email.Password);
        await smtp.SendAsync(emailz);
        await smtp.DisconnectAsync(true);
    }
}
public class Option_EmailSettings
{
    public static string SectionName = "EmailSettings";
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
