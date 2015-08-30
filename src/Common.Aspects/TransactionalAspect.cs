using System.Transactions;

using Castle.DynamicProxy;

namespace Common.Aspects
{
    public class TransactionalAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            using (var ts = new TransactionScope())
            {
                invocation.Proceed();
                var actionResult = invocation.ReturnValue as Response;
                if (actionResult == null || actionResult.Succeeded)
                {
                    //commit transaction if action result succeeded or different return type was used but no exception was thrown
                    ts.Complete();
                }
            }
        }
    }
}
