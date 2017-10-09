using MsWeb.DataObjects;
using Mingshu.Framework.MSWeb.Services;
using Mingshu.Framework.MSWeb.Services.ServiceContract;

namespace MsWeb.IServices
{
    public interface ICommonService: IBaseServiceKeyedString<UsersModel>, IServiceContract
    {
        ReturnResult<VersionInfo> GetVersionInfo();
    }
}
