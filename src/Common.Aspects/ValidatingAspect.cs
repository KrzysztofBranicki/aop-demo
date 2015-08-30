using System.Linq;

using Common.Validation;

using Castle.DynamicProxy;

namespace Common.Aspects
{
    public class ValidatingAspect : IInterceptor
    {
        private readonly IDtoValidator _validator;

        public ValidatingAspect(IDtoValidator validator)
        {
            _validator = validator;
        }

        public void Intercept(IInvocation invocation)
        {
            var validationResults = invocation.Arguments.Select(x => _validator.Validate(x)).ToList();
            if (validationResults.All(x => x.IsValid))
            {
                invocation.Proceed();
            }
            else
            {
                invocation.ReturnValue = Response.CreateFailureResponse(validationResults.First(x => !x.IsValid).Message);
            }
        }
    }
}
