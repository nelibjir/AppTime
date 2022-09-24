using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Services.FileServices
{
	public interface IFileService
	{
		Task<string> LoadFileFromDiskAsync(string source, CancellationToken cancellationToken);
		Task<Unit> WriteFileToDiskAsync(string content, string location, Encoding encoding, CancellationToken cancellationToken);
	}
}
