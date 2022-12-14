using AppTime.Handlers.Exceptions;
using System;
using System.Net;

namespace AppTime.Factories
{
	public class ApiErrorFactory
	{
		private readonly ExceptionHandlerFactory fExceptionHandlerFactory;

		public ApiErrorFactory(ExceptionHandlerFactory apiExceptionHandlerFactory)
		{
			fExceptionHandlerFactory = apiExceptionHandlerFactory;
		}

		public ApiError Create(Exception exception)
		{
			IExceptionHandler apiExceptionHandler = fExceptionHandlerFactory.Create(exception);
			if (apiExceptionHandler == null)
				return new ApiError(exception.Message, (int)HttpStatusCode.InternalServerError);

			return apiExceptionHandler.Handle(exception);
		}
	}
}
