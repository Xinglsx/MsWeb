using MsWeb.DataObjects;
using System.Threading.Tasks;
using Winning.Framework.DMSP.Services;
using Winning.Framework.DMSP.Services.ServiceContract;

namespace MsWeb.IServices
{
    public interface IAdvertisementService : IBaseServiceKeyedString<AdvertisementsModel>, IServiceContract
    {
        Task<ReturnResult<AdvertisementsModel>> GetAdvertisement(string key);
    }
}
