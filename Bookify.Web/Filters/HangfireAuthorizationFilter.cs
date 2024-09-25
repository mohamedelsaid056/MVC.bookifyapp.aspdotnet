using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Bookify.Web.Filters
// this is for using in configration security for hangfire for background jobs
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private string _policyName;

        public HangfireAuthorizationFilter(string policyName)
        {
            _policyName = policyName;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var authService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();// this is the service that we will use to make authorization for the user

            var isAuthorized = authService.AuthorizeAsync(httpContext.User, _policyName) //  httpContext.User that is make login now and what the policy that will has 
                                        .ConfigureAwait(false)
                                        .GetAwaiter()
                                        .GetResult()
                                        .Succeeded;

            return isAuthorized;
        }
    }
}