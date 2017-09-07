using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MsWeb.DataObjects;
using MsWeb.Domains;
using MsWeb.IServices;

using Winning.Framework.DMSP.Core.AspectX;
using Winning.Framework.DMSP.Core.Caching;
using Winning.Framework.DMSP.Core.Logging;
using Winning.Framework.DMSP.EFRepository;
using Winning.Framework.DMSP.Services;

namespace MsWeb.Services
{
    public class UserService : BaseService<UserModel, User>, IUserService
    {
        public UserService(IRepositoryContext context, IRepository<User> repository) : base(context, repository)
        {
        }

        public async Task<UserModel> Login(string loginName, string password)
        {
            return await Aspect.Task<UserModel>()
                .WithLog("登录")
                .WithCacheLevel2("login", loginName + "_" + password)
                .Execute(async () =>
            {

                LogHelper.LogInfo("用户【{loginName}】请求登录");
                var user = await this.FindOneAsync<UserModel, User>(repository, x => x.LoginName == loginName && x.Password == password);


                if (user == null)
                {
                    LogHelper.LogInfo("用户【{loginName}】登录失败");
                }
                else
                {
                    LogHelper.LogInfo("用户【{loginName}】登录成功");
                    return user;
                }

                return null;
            });
        }
    }
}
