using System.Transactions;

using Common;

namespace AopDemo.Application.User
{
    public class UserServiceTransactionalDecorator : IUserService
    {
        private readonly IUserService _decoratedService;

        public UserServiceTransactionalDecorator(IUserService decoratedService)
        {
            _decoratedService = decoratedService;
        }

        public Response ChangePassword(ChangePasswordRequest request)
        {
            using (var ts = new TransactionScope())
            {
                var result = _decoratedService.ChangePassword(request);
                if(result.Succeeded)
                {
                    ts.Complete();
                }

                return result;
            }
        }
    }
}