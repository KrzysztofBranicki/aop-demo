using System;

using Common;
using Common.Logging;

namespace AopDemo.Application.User
{
    public class UserServiceLoggingDecorator : IUserService
    {
        private readonly IUserService _decoratedService;
        private readonly ILogger _logger;

        public UserServiceLoggingDecorator(IUserService decoratedService, ILogger logger)
        {
            _decoratedService = decoratedService;
            _logger = logger;
        }

        public Response ChangePassword(ChangePasswordRequest request)
        {
            _logger.LogInfo("Method ChangePassword started at: " + DateTime.Now);

            var result = _decoratedService.ChangePassword(request);

            _logger.LogInfo("Method ChangePassword ended at: " + DateTime.Now);
            return result;
        }
    }
}