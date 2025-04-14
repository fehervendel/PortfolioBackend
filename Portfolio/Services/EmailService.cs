using DotNetEnv;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Portfolio.Services;

public class EmailService
{
    public void SendEmails(string userEmail, string userName, string phoneNumber, string message)
    {
        Env.Load();
        var userMessage = new MimeMessage();
        userMessage.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("ADMIN_NAME"), Environment.GetEnvironmentVariable("ADMIN_EMAIL")));
        userMessage.To.Add(new MailboxAddress(userName, userEmail));
        userMessage.Subject = "Thank you for contacting me!";

        var formattedMessage = message.Replace("\n", "<br>").Replace("\r", "");
        var userBodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
            <div style='font-family: Arial, sans-serif; color: #333;'>
                <h2>Thank you for your submission, {userName}!</h2>
                <p>I will contact you soon.</p>
                <hr>
                <p><strong>Name:</strong> {userName}</p>
                <p><strong>Email:</strong> {userEmail}</p>
                <p><strong>Phone:</strong> {phoneNumber}</p>
                <p><strong>Message:</strong></p>
                <blockquote style='padding-left: 10px; color: #555;'>
                    {formattedMessage}
                </blockquote>
                <hr>
                <p style='font-size: 14px; color: #666;'>This is an automated response. Please do not reply.</p>
            </div>",
            TextBody = $"Thank you for your submission, {userName}! I will contact you soon."
        };
        userMessage.Body = userBodyBuilder.ToMessageBody();


        var adminMessage = new MimeMessage();

        adminMessage.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("APP_NAME"), Environment.GetEnvironmentVariable("ADMIN_EMAIL")));
        adminMessage.To.Add(new MailboxAddress(Environment.GetEnvironmentVariable("ADMIN_NAME"), Environment.GetEnvironmentVariable("ADMIN_EMAIL")));
        adminMessage.Subject = "New contact from " + userName;

        var adminBodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
            <div style='font-family: Arial, sans-serif; color: #333;'>
                <h2 style='color: #c2d834;'>New Contact Received!</h2>
                <p><strong>Name:</strong> {userName}</p>
                <p><strong>Email:</strong> {userEmail}</p>
                <p><strong>Phone:</strong> {phoneNumber}</p>
                <p><strong>Message:</strong></p>
                <blockquote style='padding-left: 10px; color: #555;'>
                    {formattedMessage}
                </blockquote>
                <hr>
                <p style='font-size: 14px; color: #666;'>This is an automated notification.</p>
            </div>",
            TextBody = $@"
            New Contact Received!

            Name: {userName}
            Email: {userEmail}
            Phone: {phoneNumber}

            Message:
            {message}"
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