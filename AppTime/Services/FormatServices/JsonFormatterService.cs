using AppTime.Repositories;
using Newtonsoft.Json;
using AppTime.Exceptions;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using AppTime.Dtos;

namespace AppTime.Services
{
	public class JsonFormatterService : IJsonFormatterService
	{
		private readonly IFileExtensionRepository fFileExtensionRepository;

		public JsonFormatterService(
		  IFileExtensionRepository fileExtensionRepository
		  )
		{
			fFileExtensionRepository = fileExtensionRepository;
		}


		/// <summary>
		///  Insert dataset name
		/// </summary>
		/// <param name="name">Name of the dataset</param>
		/// <param name="cancellationToken">Cancelation token</param>
		/// <returns>Primary id of the inserted dataset</returns>
		/// <exception cref="ConflictException">Dataset with the given name is already in the table</exception>
		public string CreateJsonFromXml(string xml)
		{  
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return JsonConvert.SerializeXmlNode(doc);
		}


		public async Task<DataFileDto> GetPathsAsync(string extension_name, CancellationToken cancellationToken)
		{
			return await fFileExtensionRepository.GetPathByExtensionAsync(extension_name, cancellationToken);
		}
	}
}
