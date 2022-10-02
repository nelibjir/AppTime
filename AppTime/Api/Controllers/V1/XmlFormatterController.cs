using AppTime.Commands;
using AppTime.Enums;
using AppTime.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System;
using AppTime.Api.Models;

namespace AppTime.Api.Controllers.V1
{
	[Route("api/v1/format/xml")]
	public class XmlFormatterController : Controller
	{
		private readonly IMediator fMediator;

		public XmlFormatterController(IMediator mediator)
		{
			fMediator = mediator;
		}

		/// <summary>
		/// Creates a new XML file from the given format from query arguments, the source file is needed to be on the disk, 
		/// location is given by settings or in database table
		/// </summary>
		/// <param name="sourceFormat">Format from which we will convert to xml</param>
		/// <param name="cancellationToken">Cancellation token for the operation</param>
		/// <returns>if successful returns name of the added dataset and code 201</returns>
		/// <response code="201">Successfuly created</response>
		/// <response code="404">Format file not found between supported files.</response>
		/// <response code="400">SourceFormat parameter is missing.</response>
		[HttpPost("/")]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		public async Task<ActionResult> PostXmlFromJson(string sourceFormat, CancellationToken cancellationToken)
		{
			if (String.IsNullOrEmpty(sourceFormat))
			{
				throw new BadRequestException("outputFormat needed in query parameter!");
			}

			if (!Enum.TryParse(sourceFormat.ToUpper(), out FormatNameEnum format))
			{
				throw new NotFoundException("outputFormat is not supported!");
			}

			// TODO change the command to CreateXMLCommand
			await fMediator.Send(new CreateJsonCommand { FormatNameSource = sourceFormat }, cancellationToken);
			return CreatedAtRoute(nameof(GetNewFile), null);
		}

		/// <summary>
		/// Creates a new XML file from the given format from query arguments, the source file is in the body of the request 
		/// </summary>
		/// <param name="sourceFormat">Format from which we will convert to XML</param>
		/// <param name="source">Source which we should convert</param>
		/// <param name="cancellationToken">Cancellation token for the operation</param>
		/// <returns>if successful returns name of the added dataset and code 201</returns>
		/// <response code="201">Successfuly created</response>
		/// <response code="404">Format file not found between supported files.</response>
		[HttpPost("/")]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		public async Task<ActionResult> PostXMLFromJsonInBody(string sourceFormat, [FromBody] string source, CancellationToken cancellationToken)
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
		/// Download the file from our server
		/// </summary>
		/// <param name="name">Name of the file to be downloaded</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>if successful returns file as object with content field which is string</returns>
		/// <response code="200">Sucess</response>
		/// <response code="400">Name is missing in the request</response>
		/// <response code="404">Requested name was not found</response>
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[HttpGet]
		public async Task<File> GetNewFile(string file_name, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(file_name))
				throw new BadRequestException($"Please specify the name of the file");

			// TODO a different xmlCommand which will return xml content, we shoul also check that the requested file is XML
			return await fMediator.Send(new GetJsonFileCommand { FileName = file_name }, cancellationToken);
		}
	}
}
