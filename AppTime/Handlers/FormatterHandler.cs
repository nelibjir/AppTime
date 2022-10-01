using AppTime.Services.FileServices;
using MediatR;
using AppTime.Commands;
using AppTime.Services;
using System.Threading;
using System.Threading.Tasks;
using System;
using AppTime.Dtos;
using AppTime.Configs;

namespace AppTime.Handlers
{
    public class FormatterHandler :
      IRequestHandler<CreateJsonCommand>
    {
        private readonly IFileService fFileService;
        private readonly IJsonFormatterService fFormatService;

        public FormatterHandler(
          IJsonFormatterService formatService,
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
    }
}
