using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.Utilities
{
    public static class Utilities
    {
        public static string IsActive(this IHtmlHelper html, string control, string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control == routeControl &&
                               action == routeAction;

            return returnActive ? "active" : "";
        }
    }
}
