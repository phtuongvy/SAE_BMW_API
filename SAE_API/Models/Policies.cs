using Microsoft.AspNetCore.Authorization;

namespace SAE_API.Models
{
    public class Policies
    {
        public const string Admin = "admin";
        public const string User = "user";
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }
        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}
