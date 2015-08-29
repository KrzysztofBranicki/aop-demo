using Common;
using Common.Validation;

namespace AopDemo.Application.User
{
    public class UserServiceValidatingDecorator : IUserService
    {
        private readonly IUserService _decoratedService;
        private readonly IDtoValidator _validator;

        public UserServiceValidatingDecorator(IUserService decoratedService, IDtoValidator validator)
        {
            _decoratedService = decoratedService;
            _validator = validator;
        }

        public Response ChangePassword(ChangePasswordRequest request)
        {
            var vr = _validator.Validate(request);
            if (vr.IsValid)
                return _decoratedService.ChangePassword(request);

            return Response.CreateFailureResponse(vr.Message);
        }
    }
}