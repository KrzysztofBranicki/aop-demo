using AopDemo.DeliveryMechanism.Console.CompositionRoot;
using AopDemo.Application.User;
using NUnit.Framework;

namespace AopDemo.Tests
{
    [TestFixture]
    public class UserServiceIntegrationTests
    {
        [Test]
        public void Valid_request()
        {
            var userService = GetUserService();
            var request = new ChangePasswordRequest { UserId = 1, NewPassword = "secret!" };

            var result = userService.ChangePassword(request);

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public void Invalid_request_with_empty_password()
        {
            var userService = GetUserService();
            var request = new ChangePasswordRequest { UserId = 1 };

            var result = userService.ChangePassword(request);

            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public void Invalid_request_with_wrong_user_id()
        {
            var userService = GetUserService();
            var request = new ChangePasswordRequest { UserId = 777, NewPassword = "secret!" };

            var result = userService.ChangePassword(request);

            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public void Invalid_request_with_too_short_password()
        {
            var userService = GetUserService();
            var request = new ChangePasswordRequest { UserId = 1, NewPassword = "abc" };

            var result = userService.ChangePassword(request);

            Assert.That(result.Succeeded, Is.False);
        }

        private static IUserService GetUserService()
        {
            var container = new Bootstrapper().CreateAndConfigureContainer();
            return container.Resolve<IUserService>();
        }
    }
}
