using AopDemo.Application.User;
using AopDemo.Domain.User;
using Common.Aspects;
using Common.Logging;
using Common.Validation;

using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

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

            //Register all aspects at once
            container.Register(Classes.FromAssemblyContaining<LoggingAspect>().BasedOn<IInterceptor>().WithServiceSelf().LifestyleTransient());

            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().Interceptors(
                InterceptorReference.ForType<LoggingAspect>(),
                InterceptorReference.ForType<ValidatingAspect>(),
                InterceptorReference.ForType<ExceptionHandlingAspect>(),
                InterceptorReference.ForType<TransactionalAspect>()).First);

            return container;
        }
    }
}
