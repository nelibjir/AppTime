using AppTime.Services.FileServices;
using MediatR;
using AppTime.Api.Models;
using AppTime.Commands;
using AppTime.Services;
using System.Threading;
using System.Threading.Tasks;
using System;
using AppTime.Exceptions;
using System.Xml.Linq;
using AppTime.Dtos;
using Shipvio.Server.Supervisor.Service.Configs;

namespace AppTime.Handlers
{
    public class FormatterHandler :
      IRequestHandler<CreateJsonCommand>,
      IRequestHandler<GetDatasetNamesCommand, DatasetNames>
    {
        private readonly IFileService fFileService;
        private readonly IFormatService fFormatService;

        public FormatterHandler(
          IFormatService formatService,
          IFileService fileService
          )
        {
			fFormatService = formatService;
			fFileService = fileService;
        }

        public async Task<Unit> Handle(CreateJsonCommand request, CancellationToken cancellationToken)
        {
			DataFileDto paths = await fFormatService.GetPathsAsync(request.FormatNameSource, cancellationToken);
            if (String.IsNullOrEmpty(paths.Source)) {
                paths.Source = EnvVariables._XML_FILE_SOURCE;
			}
			if (String.IsNullOrEmpty(paths.Destination))
			{
				paths.Source = EnvVariables._JSON_FILE_DESTINATION;
			}
			string xml = await fFileService.LoadFileFromDiskAsync(paths.Source, cancellationToken);
            string json = fFormatService.CreateJsonFromXml(xml);

            await fFileService.WriteFileToDiskAsync(json, paths.Destination, null, cancellationToken);

			return Unit.Value;
        }

        public async Task<DatasetNames> Handle(GetDatasetNamesCommand request, CancellationToken cancellationToken)
        {
            return await fDatasetServicecs.GetDatasetNamesAsync(cancellationToken);
        }
    }
}
