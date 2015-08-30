using System;
using Common.Logging;
using Castle.DynamicProxy;

namespace Common.Aspects
{
    public class LoggingAspect : IInterceptor
    {
        private readonly ILogger _logger;

        public LoggingAspect(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _logger.LogInfo(string.Format("Method {0} started at: {1}", invocation.Method.Name, DateTime.Now));

            invocation.Proceed();

            _logger.LogInfo(string.Format("Method {0} ended at: {1}", invocation.Method.Name, DateTime.Now));
        }
    }
}
