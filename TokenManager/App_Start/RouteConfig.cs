using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace TokenManager
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
            routes.Ignore("ChartImg.axd/{*pathInfo}");
            routes.MapPageRoute(
                "HomeRoute",
                "",
                "~/Views/Token/Token.aspx"
            );

            routes.MapPageRoute(
                "TokenDetail",
                "detail/{symbol}",
                "~/Views/Token/TokenDetail.aspx"
            );
        }
    }
}
