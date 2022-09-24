using AppTime.Dtos;
using AppTime.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Repositories
{
	public interface IFileExtensionRepository : IGenericRepository<FileExtension>
	{
		Task<DataFileDto> GetPathByExtensionAsync(string extension_name, CancellationToken cancellationToken);
	}
}
