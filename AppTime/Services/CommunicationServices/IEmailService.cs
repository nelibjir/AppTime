using System.Threading.Tasks;
using System.Threading;

namespace AppTime.Services.CommunicationServices
{
	public interface IEmailService
	{
		Task SendEmailAsync(string text, string recipient, CancellationToken cancellationToken);
	}
}
