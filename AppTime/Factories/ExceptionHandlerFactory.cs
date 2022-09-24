using AppTime.Handlers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTime.Factories
{
	public class ExceptionHandlerFactory
	{
		private readonly IEnumerable<IExceptionHandler> fExceptionHandlers;

		public ExceptionHandlerFactory(IEnumerable<IExceptionHandler> exceptionHandlers)
		{
			fExceptionHandlers = exceptionHandlers;
		}

		public IExceptionHandler Create(Exception ex)
		{
			return fExceptionHandlers.FirstOrDefault(x => x.CanHandle(ex));
		}
	}
}
