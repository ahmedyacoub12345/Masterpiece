using System.Net.Mail;
using System.Net;

namespace MasterPeiceBackEnd.Shared
{
    public static class EmailSender
    {
        public static void SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                const string fromAddress = "jordainianelection@gmail.com";

                // Set up the Gmail SMTP client with the app password
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("jordainianelection@gmail.com", "wsvl ilbd hdqd ooap"), // Use your app password here
                    EnableSsl = true
                };

                // Create a MailMessage object
                var mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}
