using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MsWeb.DataObjects;

using Winning.Framework.DMSP.Services;

namespace MsWeb.IServices
{
    public interface IUserService : IBaseService<UserModel>, IServiceContract
    {
        Task<UserModel> Login(string loginName, string password);
    }
}
