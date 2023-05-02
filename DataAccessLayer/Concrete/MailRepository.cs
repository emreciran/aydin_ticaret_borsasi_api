using DataAccessLayer.Abstract;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class MailRepository : IMailRepository
    {
        public MailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IConfiguration _configuration { get; }


        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Test", _configuration["Email"]));
            email.To.Add(new MailboxAddress(toEmail, toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(_configuration["Email"], _configuration["EmailPsw"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
