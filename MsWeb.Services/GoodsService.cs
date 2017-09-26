using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MsWeb.DataObjects;
using MsWeb.IServices;
using Winning.Framework.DMSP.Core.Paging;
using Winning.Framework.DMSP.Core.AspectX;
using MsWeb.Domains;
using Winning.Framework.DMSP.Services;
using Winning.Framework.DMSP.EFRepository;

namespace MsWeb.Services
{
    public class GoodsService : BaseServiceKeyedString<GoodsModel, Goods>, IGoodsService
    {
        public GoodsService(IRepositoryContext context, IRepository<Goods> repository) : base(context, repository)
        {
        }

        public Task<bool> Add(GoodsModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GoodsModel>> Get(Expression<Func<GoodsModel, bool>> filter = null, Expression<Func<GoodsModel, object>> sortField = null, SortOrder sortOrder = SortOrder.Unspecified)
        {
            throw new NotImplementedException();
        }

        public Task<List<GoodsModel>> GetAll(Expression<Func<GoodsModel, object>> sortField = null, SortOrder sortOrder = SortOrder.Unspecified)
        {
            throw new NotImplementedException();
        }

        public Task<GoodsModel> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ReturnResult<List<GoodsModel>>> GetRecommandGoodsList(int curPage, int pageSize, int type, string filter)
        {
            List<int> states = new List<int>();
            if (type == 10)
            {
                states.Add(0);
                states.Add(1);
            }
            else if (type == 9)
            {
                states.Add(0);
                states.Add(1);
                states.Add(2);
                states.Add(3);
            }
            else if (type == 310)
            {
                states.Add(0);
                states.Add(1);
                states.Add(3);
            }
            else
            {
                states.Add(type);
            }
            if (string.IsNullOrEmpty(filter)) { filter = string.Empty; }

            if (pageSize <= 0) pageSize = 10;//默认取10条

            return await Aspect.Task<ReturnResult<List<GoodsModel>>>(async () =>
            {
                ReturnResult<List<GoodsModel>> result = new ReturnResult<List<GoodsModel>>();

                List<GoodsModel> goods = await this.FindResultAsync<GoodsModel, Goods>(repository, x =>
                    states.Contains(x.state ?? 0) && (filter == string.Empty || x.description.Contains(filter)), order:SortOrder.Descending);

                if (goods == null || goods.Count == 0)
                {
                    result.code = -105;
                    result.message = "已加载完。";
                    return result;
                }
                result.code = 1;
                result.data = goods;
                return result;
            })
                .WithLog("获取商品列表")
                .Execute();

        }

        public Task<GoodsModel> GetOne(Expression<Func<GoodsModel, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedData<GoodsModel>> GetPage(int pageIndex, int pageSize, Expression<Func<GoodsModel, bool>> filter = null, Dictionary<Expression<Func<GoodsModel, object>>, SortOrder> sorts = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(GoodsModel model)
        {
            throw new NotImplementedException();
        }
    }
}
