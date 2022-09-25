using System.Threading.Tasks;
using System.Threading;
using System.Net.Mail;

namespace AppTime.Services.CommunicationServices
{
	public class EmailService : IEmailService
	{
		public async Task SendEmailAsync(string text, string recipient, CancellationToken cancellationToken)
		{
			using (MailMessage mail = new MailMessage())
			{
				using (SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com")) // TODO add to settings
				{
					mail.From = new MailAddress("app_time@gmail.com");  // TODO add to settings
					mail.To.Add(recipient);
					mail.Subject = "Test Mail - 1"; // TODO add to settings
					mail.Body = text;

					Attachment attachment;
					attachment = new System.Net.Mail.Attachment(text);
					mail.Attachments.Add(attachment);

					SmtpServer.Port = 587;
					SmtpServer.Credentials = new System.Net.NetworkCredential("app_time@gmail.com", "your password");
					SmtpServer.EnableSsl = true;

					await SmtpServer.SendMailAsync(mail);
				}
			}

		}
	}
}
