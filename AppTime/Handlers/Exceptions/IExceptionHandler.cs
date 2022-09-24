using System;

namespace AppTime.Handlers.Exceptions
{
	public interface IExceptionHandler
	{
		bool CanHandle(Exception ex);

		ApiError Handle(Exception ex);
	}
}
