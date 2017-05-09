using SmartMarket.BLL.ViewModels;
using System.Net;
using System.Net.Mail;


namespace SmartMarket.BLL.Services
{
    public static class EmailService
    {
        public static void Send(UserModel user, string subject, string body)
        {
            // TODO put credantionals from email to condig.

            var from = new MailAddress("Kokorin1506@gmail.com", "Pavel Kokorin");
            var password = "7131498Alex";

            var to = new MailAddress(user.Email, user.Name);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, password)
            };

            var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body
            };

            smtp.Send(message);
        }
    }
}
