using Common;

namespace AopDemo.Application.User
{
    public interface IUserService
    {
        Response ChangePassword(ChangePasswordRequest request);
    }
}