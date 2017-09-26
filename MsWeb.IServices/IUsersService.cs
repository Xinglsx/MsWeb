using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MsWeb.DataObjects;

using Winning.Framework.DMSP.Services;
using Winning.Framework.DMSP.Services.ServiceContract;

namespace MsWeb.IServices
{
    public interface IUsersService : IBaseServiceKeyedString<UsersModel>, IServiceContract
    {
        Task<UsersModel> Login(string loginName, string password);
    }
}
