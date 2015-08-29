using System.Transactions;
using AopDemo.Domain.User;
using Common;

namespace AopDemo.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordStrengthValidator _passwordStrengthValidator;

        public UserService(IUserRepository userRepository, IPasswordStrengthValidator passwordStrengthValidator)
        {
            _userRepository = userRepository;
            _passwordStrengthValidator = passwordStrengthValidator;
        }

        public Response ChangePassword(ChangePasswordRequest request)
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

            return Response.CreateSuccessfulResponse();
        }
    }
}
