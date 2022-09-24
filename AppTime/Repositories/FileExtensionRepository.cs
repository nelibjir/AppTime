using AppTime.Dtos;
using AppTime.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Repositories
{
	public class FileExtensionRepository : GenericRepository<FileExtension>, IFileExtensionRepository
	{
		public FileExtensionRepository(DataContext dataContext) : base(dataContext) { }

		public async Task<DataFileDto> GetPathByExtensionAsync(string extension_name, CancellationToken cancellationToken)
		{
			return await DbSet
				.Where(fileExtension => fileExtension.FormatName == extension_name)
				.Where(fileExtension => fileExtension.IsLocal == true)
				.Select(fileExtension => new DataFileDto { 
					Source = fileExtension.SourcePath, 
					Destination = fileExtension.DestinationPath
				})
				.FirstAsync(cancellationToken);
		}
	}
}
