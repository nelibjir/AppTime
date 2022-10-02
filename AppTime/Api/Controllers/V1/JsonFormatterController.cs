using MediatR;
using Microsoft.AspNetCore.Mvc;
using AppTime.Api.Models;
using AppTime.Commands;
using AppTime.Exceptions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AppTime.Enums;

namespace AppTime.Api.Controllers.V1
{
    [Route("api/v1/format/json")]
    public class JsonFormatterController : Controller
    {
        private readonly IMediator fMediator;

        public JsonFormatterController(IMediator mediator)
        {
            fMediator = mediator;
        }

		/// <summary>
		/// Creates a new json file from the given format from query arguments, the source file is needed to be on the disk, 
		/// location is given by settings or in database table
		/// </summary>
		/// <param name="sourceFormat">Format from which we will convert to json</param>
		/// <param name="cancellationToken">Cancellation token for the operation</param>
		/// <returns>if successful returns code 201</returns>
		/// <response code="201">Successfuly created</response>
		/// <response code="404">Format file not found between supported files.</response>
		/// <response code="400">SourceFormat parameter is missing.</response>
		[HttpPost("/")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> PostJsonFromXml(string sourceFormat, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(sourceFormat))
            {
                throw new BadRequestException("outputFormat needed in query parameter!");
            }

            if (!Enum.TryParse(sourceFormat.ToUpper(), out FormatNameEnum format))
            {
                throw new NotFoundException("outputFormat is not supported!");
            }

			await fMediator.Send(new CreateJsonCommand { FormatNameSource = sourceFormat }, cancellationToken);
			return CreatedAtRoute(nameof(GetNewFile), null);
		}

		/// <summary>
		/// Creates a new json file from the given format from query arguments, the source file is in the body of the request 
		/// </summary>
		/// <param name="sourceFormat">Format from which we will convert to json</param>
		/// <param name="source">Source which we should convert</param>
		/// <param name="cancellationToken">Cancellation token for the operation</param>
		/// <returns>if successful returns code 201</returns>
		/// <response code="201">Suceesfuly created</response>
		/// <response code="404">Format file not found between supported files.</response>
		[HttpPost("/")]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		public async Task<ActionResult> PostJsonFromXmlInBody(string sourceFormat, [FromBody] string source, CancellationToken cancellationToken)
		{
			if (String.IsNullOrEmpty(sourceFormat))
			{
				throw new BadRequestException("outputFormat needed in query parameter!");
			}

			if (String.IsNullOrWhiteSpace(source))
			{
				return Ok(); // we don't have anything to convert
			}

			if (!Enum.TryParse(sourceFormat.ToUpper(), out FormatNameEnum format))
			{
				throw new NotFoundException("outputFormat is not supported!");
			}

			// TODO now we will change the command only and handler method, so it would call correct method
			// with source parameter from body
			await fMediator.Send(new CreateJsonCommand { FormatNameSource = sourceFormat }, cancellationToken);
			return CreatedAtRoute(nameof(GetNewFile), null);
		}

		/// <summary>
		/// Creates a new json file from the given format from query arguments, the source file is as text in query parameter
		/// </summary>
		/// <param name="sourceFormat">Format from which we will convert to json</param>
		/// <param name="cancellationToken">Cancellation token for the operation</param>
		/// <returns>if successful returns code 201</returns>
		/// <response code="201">Suceesfuly created</response>
		/// <response code="404">Format file not found between supported files..</response>
		/// <response code="400">SourceFormat parameter is missing.</response>
		[HttpPost("/")]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		public async Task<ActionResult> PostJsonFromXmlFromUrl(string sourceFormat, string text, CancellationToken cancellationToken)
		{
			if (String.IsNullOrEmpty(sourceFormat))
			{
				throw new BadRequestException("outputFormat needed in query parameter!");
			}

			if (!Enum.TryParse(sourceFormat.ToUpper(), out FormatNameEnum format))
			{
				throw new NotFoundException("outputFormat is not supported!");
			}

			if (String.IsNullOrWhiteSpace(text))
			{
				return Ok(); // we don't have anything to convert
			}

			// TODO make new command so it would chose correct method in handler to precess argument from query 
			// as theis can have special logic - as for example we will not save them
			await fMediator.Send(new CreateJsonCommand { FormatNameSource = sourceFormat }, cancellationToken);
			return CreatedAtRoute(nameof(GetNewFile), null);
		}

		/// <summary>
		/// Download the file from our server.
		/// </summary>
		/// <param name="name">Name of the file to be downloaded</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>if successful returns file as object with content field which is string and code 200.</returns>
		/// <response code="200">Sucess</response>
		/// <response code="400">Name is missing in the request</response>
		/// <response code="404">Requested name was not found</response>
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[HttpGet]
        public async Task<File> GetNewFile(string fileName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new BadRequestException($"Please specify the name of the file");

            return await fMediator.Send(new GetJsonFileCommand { FileName = fileName }, cancellationToken);
        }
    }
}
