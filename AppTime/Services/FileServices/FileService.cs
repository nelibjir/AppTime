using MediatR;

using System.IO;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Services.FileServices
{
	public class FileService : IFileService
	{
		// closes the file so no need to use using
		public async Task<string> LoadFileFromDiskAsync(string source, CancellationToken cancellationToken) => await File.ReadAllTextAsync(source, cancellationToken);
		public async Task<Unit> WriteFileToDiskAsync(string content, string location, Encoding encoding, CancellationToken cancellationToken)
		{
			if (encoding == null) encoding = Encoding.UTF8;
			// closes the file so no need to use using
			await File.WriteAllTextAsync(location, content, encoding, cancellationToken);
			return Unit.Value;
		}
	}
}
