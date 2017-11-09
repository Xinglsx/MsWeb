using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mingshu.Framework.MSWeb.Services;

using MsWeb.DataObjects;
using Mingshu.Framework.MSWeb.Services.ServiceContract;
using Mingshu.Framework.MSWeb.Core.Paging;

namespace MsWeb.IServices
{
    public interface IGoodsService : IBaseServiceKeyedString<GoodsModel>, IServiceContract
    {
        Task<ReturnResult<PagedData<GoodsModel>>> GetRecommandGoodsList(int curPage, int pageSize, int type, string filter);

        Task<ReturnResult<bool>> ClickCounIncrement(string id);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userInfo">待更新用户信息</param>
        /// <returns></returns>
        Task<ReturnResult<bool>> SaveGoodsInfo(GoodsModel goodsInfo);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userInfo">待更新用户信息</param>
        /// <returns></returns>
        Task<ReturnResult<bool>> AuditGoodsInfo(GoodsModel goodsInfo);
        /// <summary>
        /// 通过商品ID获取商品详细信息
        /// </summary>
        /// <param name="id">商品主键</param>
        /// <returns></returns>
        Task<ReturnResult<GoodsModel>> GetGoodsInfo(string id);
    }
}
