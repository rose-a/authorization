using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Authorization
{
    public class AuthorizationPolicyBuilder
    {
        private readonly List<IAuthorizationRequirement> _requirements;

        public AuthorizationPolicyBuilder()
        {
            _requirements = new List<IAuthorizationRequirement>();
        }

        public AuthorizationPolicy Build()
        {
            var policy = new AuthorizationPolicy(_requirements);
            return policy;
        }

        public AuthorizationPolicyBuilder RequireClaim(string claimType)
        {
            var requirement = new ClaimAuthorizationRequirement(claimType);
            _requirements.Add(requirement);
            return this;
        }

        public AuthorizationPolicyBuilder RequireClaim(string claimType, params string[] allowedValues)
        {
            var requirement = new ClaimAuthorizationRequirement(claimType, allowedValues);
            _requirements.Add(requirement);
            return this;
        }

        public AuthorizationPolicyBuilder RequireClaim(string claimType, IEnumerable<string> allowedValues, IEnumerable<string> displayValues)
        {
            var requirement = new ClaimAuthorizationRequirement(claimType, allowedValues, displayValues);
            _requirements.Add(requirement);
            return this;
        }

        public AuthorizationPolicyBuilder RequireAssertion(Action<AuthorizationContext> assertAction)
        {
            return AddRequirement(new AssertionRequirement(assertAction));
        }

        public AuthorizationPolicyBuilder RequireAssertion(Func<AuthorizationContext, Task> assertTask)
        {
            return AddRequirement(new AssertionRequirement(assertTask));
        }

        public AuthorizationPolicyBuilder AddRequirement(IAuthorizationRequirement requirement)
        {
            _requirements.Add(requirement);
            return this;
        }

    }
}
