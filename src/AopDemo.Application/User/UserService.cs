using System;
using System.Transactions;
using AopDemo.Domain.User;
using Common;
using Common.Logging;
using Common.Validation;

namespace AopDemo.Application.User
{
    public class UserService
    {
        private readonly IDtoValidator _validator;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordStrengthValidator _passwordStrengthValidator;

        public UserService(IDtoValidator validator, ILogger logger, IUserRepository userRepository, IPasswordStrengthValidator passwordStrengthValidator)
        {
            _validator = validator;
            _logger = logger;
            _userRepository = userRepository;
            _passwordStrengthValidator = passwordStrengthValidator;
        }

        public Response ChangePassword(ChangePasswordRequest request)
        {
            _logger.LogInfo("Method ChangePassword started at: " + DateTime.Now);

            var vr = _validator.Validate(request);
            if (!vr.IsValid)
                return Response.CreateFailureResponse(vr.Message);

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

            _logger.LogInfo("Method ChangePassword ended at: " + DateTime.Now);

            return Response.CreateSuccessfulResponse();
        }
    }
}
