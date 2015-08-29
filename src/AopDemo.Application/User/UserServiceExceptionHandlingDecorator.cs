using System;

using Common;
using Common.Logging;

namespace AopDemo.Application.User
{
    public class UserServiceExceptionHandlingDecorator : IUserService
    {
        private readonly IUserService _decoratedService;
        private readonly ILogger _logger;

        public UserServiceExceptionHandlingDecorator(IUserService decoratedService, ILogger logger)
        {
            _decoratedService = decoratedService;
            _logger = logger;
        }

        public Response ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                return _decoratedService.ChangePassword(request);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception);
                return Response.CreateFailureResponse(exception.Message);
            }
        }
    }
}