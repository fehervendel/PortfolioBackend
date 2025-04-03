using DotNetEnv;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Portfolio.Services;

public class EmailService
{
    public void SendEmails(string userEmail, string userName)
    {
        Env.Load();
        var userMessage = new MimeMessage();
        userMessage.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("ADMIN_NAME"), Environment.GetEnvironmentVariable("ADMIN_EMAIL")));
        userMessage.To.Add(new MailboxAddress(userName, userEmail));
        userMessage.Subject = "Thank you for contacting me!";

        var userBodyBuilder = new BodyBuilder
        {
            HtmlBody = "<h1>Thank you for your submission!</h1><p>I will contact you soon.</p>",
            TextBody = "Thank you for your submission! I will contact you soon."
        };
        userMessage.Body = userBodyBuilder.ToMessageBody();


        var adminMessage = new MimeMessage();

        adminMessage.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("APP_NAME"), Environment.GetEnvironmentVariable("ADMIN_EMAIL")));
        adminMessage.To.Add(new MailboxAddress(Environment.GetEnvironmentVariable("ADMIN_NAME"), Environment.GetEnvironmentVariable("ADMIN_EMAIL")));
        adminMessage.Subject = "New contact from " + userName;

        var adminBodyBuilder = new BodyBuilder
        {
            HtmlBody = $"<h1>New contact!</h1><p>You've received a new contact from {userName}</p>",
            TextBody = $"You've received a new contact from {userName}"
        };
        adminMessage.Body = adminBodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate(Environment.GetEnvironmentVariable("ADMIN_EMAIL"), Environment.GetEnvironmentVariable("EMAIL_APP_PASSWORD"));
                client.Send(userMessage);
                client.Send(adminMessage);
                client.Disconnect(true);

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
        }
    }
}