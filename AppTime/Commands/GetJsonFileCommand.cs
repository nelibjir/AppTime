using AppTime.Api.Models;
using MediatR;

namespace AppTime.Commands
{
	public class GetJsonFileCommand : IRequest<File>
	{
		public string FileName { get; set; }
	}
}
