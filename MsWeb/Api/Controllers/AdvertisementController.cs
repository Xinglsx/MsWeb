using MsWeb.DataObjects;
using MsWeb.IServices;
using MsWeb.Services;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mingshu.Framework.MSWeb.Web.WebApi;

namespace MsWeb.Api.Controllers
{
    public class AdvertisementController : WebApiController<AdvertisementsModel, IAdvertisementService>
    {
        [HttpGet]
        public async Task<ReturnResult<AdvertisementsModel>> GetAdvertisement(string key)
        {
            return await Service.GetAdvertisement(key);
        }
    }
}