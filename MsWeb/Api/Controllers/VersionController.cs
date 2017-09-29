using MsWeb.DataObjects;
using MsWeb.IServices;
using System.Web.Mvc;
using Winning.Framework.DMSP.Web.WebApi;

namespace MsWeb.Api.Controllers
{
    public class VersionController : WebApiController<UsersModel, ICommonService>
    {
        [HttpGet]
        public ReturnResult<VersionInfo> GetVersionInfo()
        {
            return Service.GetVersionInfo();
        }
    }
}