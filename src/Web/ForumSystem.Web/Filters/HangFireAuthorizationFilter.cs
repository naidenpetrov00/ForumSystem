namespace ForumSystem.Hubs.Filters
{
    using System;

    using Hangfire.Dashboard;

    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.IsInRole("Admin");
        }
    }
}
