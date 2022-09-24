using MediatR;
using AppTime.Api.Models;

namespace AppTime.Commands
{
	public class GetDatasetNamesCommand : IRequest<DatasetNames>
	{
	}
}
