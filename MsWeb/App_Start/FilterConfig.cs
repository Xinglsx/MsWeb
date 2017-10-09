using System.Web;
using System.Web.Mvc;

using Mingshu.Framework.MSWeb.Web.Filters;

namespace MsWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcExceptionFilter());
        }
    }
}
