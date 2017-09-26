using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winning.Framework.DMSP.Services;

using MsWeb.DataObjects;
using Winning.Framework.DMSP.Services.ServiceContract;

namespace MsWeb.IServices
{
    public interface IGoodsService : IBaseServiceKeyedString<GoodsModel>, IServiceContract
    {
        Task<ReturnResult<List<GoodsModel>>> GetRecommandGoodsList(int curPage, int pageSize, int type, string filter);
    }
}
