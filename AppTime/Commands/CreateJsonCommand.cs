using MediatR;

namespace AppTime.Commands
{
	public class CreateJsonCommand : IRequest
	{
		public string FormatNameSource { get; set; }
	}
}
