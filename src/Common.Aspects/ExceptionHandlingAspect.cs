using System;

using Common.Logging;

using Castle.DynamicProxy;

namespace Common.Aspects
{
    public class ExceptionHandlingAspect : IInterceptor
    {
        private readonly ILogger _logger;

        public ExceptionHandlingAspect(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception);
                invocation.ReturnValue = Response.CreateFailureResponse(exception.Message);
            }
        }
    }
}
