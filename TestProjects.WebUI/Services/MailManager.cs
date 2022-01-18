using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MimeKit.Text;
using System.IO;
using System.Linq;

namespace TestProjects.WebUI.Services
{
    public class MailManager : IMailServices
    {
        private IHostingEnvironment _hostingEnvironment;
        private IEmailConfiguration _emailConfigation;
        public MailManager(IHostingEnvironment  hostingEnvironment,IEmailConfiguration emailConfiguration)
        {
            _hostingEnvironment= hostingEnvironment;
            _emailConfigation= emailConfiguration;
        }
        public void Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x=>new MailboxAddress(x.Name, x.Address)));

            message.Subject=emailMessage.Subject;

            var templatePath = _hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "MailTemplate" + Path.DirectorySeparatorChar.ToString() + "MailTemplate.html";
            var builder=new BodyBuilder();
            using (StreamReader sourceReader=File.OpenText(templatePath))
            {
                builder.HtmlBody=sourceReader.ReadToEnd();
            }
            string messageBody = string.Format(builder.HtmlBody, emailMessage.Subject, emailMessage.Content);

            message.Body = new TextPart(TextFormat.Html) 
            {
                Text= messageBody
            };
            using (var emailClient=new SmtpClient())
            {
                emailClient.Connect(_emailConfigation.SmtpServer, _emailConfigation.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);
            }

        }
    }
}
