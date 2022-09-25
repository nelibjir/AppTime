using AppTime.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Services
{
	public interface IJsonFormatterService
	{
		string CreateJsonFromXml(string xml);
		Task<DataFileDto> GetPathsAsync(string name, CancellationToken cancellationToken);
	}
}
