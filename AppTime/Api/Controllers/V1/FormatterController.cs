using MediatR;
using Microsoft.AspNetCore.Mvc;
using AppTime.Api.Models;
using AppTime.Commands;
using AppTime.Exceptions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Api.Controllers.V1
{
    [Route("api/v1/fromatt")]
    public class FormatterController : Controller
    {
        private readonly IMediator fMediator;

        public FormatterController(IMediator mediator)
        {
            fMediator = mediator;
        }

        /// <summary>
        /// Creates a new json file from the given format from query arguments, the source file is needed to be on the disk, 
        /// location given by settings
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation</param>
        /// <returns>if successful returns name of the added dataset and code 201</returns>
        /// <response code="201">The request is not correct, excpected FileForm and only one file to be sent with its name</response>
        /// <response code="404">XML file not found on disk.</response>
        [HttpPost("/json")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> PostJsonFromXml(string outputFormat, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(outputFormat))
            {
                throw new BadRequestException("outputFormat needed in query parameter!");
            }

			// throw new NotFoundException(request.FormatNameSource); if not in enum 
			await fMediator.Send(new CreateJsonCommand { FormatNameSource = outputFormat }, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Get name of all datasets already in our system
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Names of all dataset in our system </returns>
        [HttpGet]
        public async Task<DatasetNames> GetNameOfDatasets(CancellationToken cancellationToken)
        {
            return await fMediator.Send(new GetDatasetNamesCommand { }, cancellationToken);
        }

        /// <summary>
        /// Get statistics for the named dataset
        /// </summary>
        /// <param name="name">Name of dataset for the</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Average number of friends and number of users in that dataset</returns>
        /// <response code="400">Name is missing in the request</response>
        /// <response code="404">Requested name was not found</response>
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet, Route("{name}", Name = nameof(GetNewFile))]
        public async Task<DatasetStatistics> GetNewFile(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
                throw new BadRequestException($"Please specify the name of the dataset");

            return await fMediator.Send(new GetDatasetStatisticsCommand { Name = name }, cancellationToken);
        }
    }
}
