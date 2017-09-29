using MsWeb.DataObjects;
using Winning.Framework.DMSP.Services;
using Winning.Framework.DMSP.Services.ServiceContract;

namespace MsWeb.IServices
{
    public interface ICommonService: IBaseServiceKeyedString<UsersModel>, IServiceContract
    {
        ReturnResult<VersionInfo> GetVersionInfo();
    }
}
