using System;
using System.Transactions;
using AopDemo.Domain.User;
using Common;
using Common.Logging;
using Common.Validation;

namespace AopDemo.Application.User
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordStrengthValidator _passwordStrengthValidator;

        public UserService(ILogger logger, IUserRepository userRepository, IPasswordStrengthValidator passwordStrengthValidator)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordStrengthValidator = passwordStrengthValidator;
        }

        public Response ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    var user = _userRepository.Get(request.UserId);
                    if (!_passwordStrengthValidator.IsPasswordGoodEnough(request.NewPassword))
                        return Response.CreateFailureResponse("too weak password");

                    user.ChangePassword(request.NewPassword);
                    _userRepository.Update(user);

                    ts.Complete();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception);
                return Response.CreateFailureResponse(exception.Message);
            }

            return Response.CreateSuccessfulResponse();
        }
    }
}
