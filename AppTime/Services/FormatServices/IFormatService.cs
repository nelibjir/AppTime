using AppTime.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Services
{
	public interface IFormatService
	{
		string CreateJsonFromXml(string name);
		Task<DataFileDto> GetPathsAsync(string name, CancellationToken cancellationToken);
	}
}
