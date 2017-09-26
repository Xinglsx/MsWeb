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
    public class UsersService : BaseServiceKeyedString<UsersModel, Users>, IUsersService
    {
        public UsersService(IRepositoryContext context, IRepository<Users> repository) : base(context, repository)
        {
        }

        public async Task<UsersModel> Login(string loginName, string password)
        {
            return null;
        }
    }
}
