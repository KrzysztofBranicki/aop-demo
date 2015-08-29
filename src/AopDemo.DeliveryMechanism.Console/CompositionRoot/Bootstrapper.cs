using AopDemo.Application.User;
using AopDemo.Domain.User;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Logging;
using Common.Validation;

namespace AopDemo.DeliveryMechanism.Console.CompositionRoot
{
    public class Bootstrapper
    {
        public IWindsorContainer CreateAndConfigureContainer()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<ILogger>().ImplementedBy<ConsoleLogger>());
            container.Register(Component.For<IDtoValidator>().ImplementedBy<DataAnnotationsDtoValidator>());
            container.Register(Component.For<IPasswordStrengthValidator>().ImplementedBy<DummyPasswordStrengthValidator>());
            container.Register(Component.For<IUserRepository>().ImplementedBy<DummyUserRepository>());

            container.Register(Component.For<IUserService>().ImplementedBy<UserServiceLoggingDecorator>());
            container.Register(Component.For<IUserService>().ImplementedBy<UserServiceValidatingDecorator>());
            container.Register(Component.For<IUserService>().ImplementedBy<UserServiceExceptionHandlingDecorator>());
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>());

            return container;
        }
    }
}
