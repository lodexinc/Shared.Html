using System;
using System.Security.Principal;

namespace Shared.Html
{
    public static class UserHelper
    {
        public static string StripDomain(IPrincipal user)
        {
            // Parse Windows name from domain if needed
            var userName = user.Identity.Name;
            userName = userName.Remove(0, userName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
            return userName;
        }

        public static string StripDomain(string userName)
        {
            // Parse Windows name from domain if needed
            userName = userName.Remove(0, userName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
            return userName;
        }
    }
}