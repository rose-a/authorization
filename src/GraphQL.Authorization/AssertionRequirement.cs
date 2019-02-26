using System;
using System.Threading.Tasks;

namespace GraphQL.Authorization
{
    public class AssertionRequirement: IAuthorizationRequirement
    {
        private readonly Func<AuthorizationContext, Task> _assertTask;
        private readonly Action<AuthorizationContext> _assertAction;

        public AssertionRequirement(Action<AuthorizationContext> assertAction)
        {
            _assertAction = assertAction ?? throw new ArgumentNullException(nameof(assertAction));
        }

        public AssertionRequirement(Func<AuthorizationContext, Task> assertTask)
        {
            _assertTask = assertTask ?? throw new ArgumentNullException(nameof(assertTask));
        }

        public Task Authorize(AuthorizationContext context)
        {
            if (_assertTask != null) return _assertTask(context);

            _assertAction(context);
            return Task.CompletedTask;
        }
    }
}
