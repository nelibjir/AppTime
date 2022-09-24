using System.Net;

namespace AppTime.Exceptions
{
	public class BadRequestException : ApiException
	{
		public BadRequestException(string message)
		  : base(HttpStatusCode.BadRequest, message) { }
	}
}
