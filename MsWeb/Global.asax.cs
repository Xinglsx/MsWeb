using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Winning.Framework.DMSP.Core.Infrastructure.Engine;
using Winning.Framework.DMSP.Core.TypeFinder;
using Winning.Framework.DMSP.DataAccess;

namespace MsWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //var a = new DDD("",null,null);

            EngineContext.Initialize();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    class DDD : DbContext
    {
        public DDD(string name, string connstr, ITypeFinder finder) : base(name, connstr, finder)
        {
        }
    }
}
