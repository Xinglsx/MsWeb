﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MsWeb.DataObjects;
using MsWeb.IServices;
using Mingshu.Framework.MSWeb.Core.Paging;
using Mingshu.Framework.MSWeb.Core.AspectX;
using MsWeb.Domains;
using Mingshu.Framework.MSWeb.Services;
using Mingshu.Framework.MSWeb.EFRepository;
using MsWeb.Core.Utils;

namespace MsWeb.Services
{
    public class GoodsService : BaseServiceKeyedString<GoodsModel, Goods>, IGoodsService
    {
        public GoodsService(IRepositoryContext context, IRepository<Goods> repository) : base(context, repository)
        {
        }

        /// <summary>
        /// 分页加载商品信息列表
        /// </summary>
        /// <param name="curPage">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="type">商品类型</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public async Task<ReturnResult<PagedData<GoodsModel>>> GetRecommandGoodsList(int curPage, int pageSize, int type, string filter)
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

            return await Aspect.Task<ReturnResult<PagedData<GoodsModel>>>(async () =>
            {
                ReturnResult<PagedData<GoodsModel>> result = new ReturnResult<PagedData<GoodsModel>>();


                Expression<Func<Goods, bool>> exp = x => states.Contains(x.state ?? 0) && (filter == string.Empty || x.description.Contains(filter));
                
                var goods = await this.FindResultWithPagingAsync<GoodsModel, Goods>(repository, curPage, pageSize, exp,
                    x => new { x.recommendtime }, SortOrder.Descending);

                if (goods == null || goods.DataList == null || goods.DataList.Count == 0)
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


        public async Task<ReturnResult<bool>> ClickCounIncrement(string id)
        {
            return await Aspect.Task<ReturnResult<bool>>(async () =>
            {
                ReturnResult<bool> result = new ReturnResult<bool>();

                result.data= await this.Update(id, x => x.clickcount++);
                result.code = 1;
                return result;
            })
            .WithLog("商品点击数自增")
            .Execute();
        }

        public async Task<ReturnResult<bool>> SaveGoodsInfo(GoodsModel goodsInfo)
        {
            LogUtil.WebLog("进入SaveGoodsInfo");
            return await Aspect.Task(async () =>
            {
                ReturnResult<bool> result = new ReturnResult<bool>();
                //新增
                if (string.IsNullOrEmpty(goodsInfo.ID))
                {
                    goodsInfo.ID = Guid.NewGuid().ToString();
                    try
                    {
                        goodsInfo.image = ImageUtil.StrToUri(goodsInfo.image, goodsInfo.ID + ".jpg");
                        goodsInfo.buyimage = ImageUtil.StrToUri(goodsInfo.buyimage, goodsInfo.ID + "_buy.jpg");
                    }
                    catch (Exception exp)
                    {
                        LogUtil.WebError(exp);
                        //可以增加默认图片的加载
                    }
                    
                    goodsInfo.recommendtime = DateTime.Now;
                    if(goodsInfo.state == 2)
                    {
                        goodsInfo.audittime = DateTime.Now;
                    }
                    result.data = await this.Add(goodsInfo);
                    if (result.data)
                    {
                        result.code = 1;
                    }
                    else
                    {
                        result.code = -106;
                        result.message = "商品信息新增失败！";
                    }
                    return result;
                }
                //修改
                else
                {
                    //修改的商品，可能已经是地址模式化
                    if (goodsInfo.image != null && !goodsInfo.image.Contains(".jpg"))
                    {
                        goodsInfo.image = ImageUtil.StrToUri(goodsInfo.image, goodsInfo.ID + ".jpg");
                    }
                    if (goodsInfo.buyimage != null && !goodsInfo.buyimage.Contains("_buy.jpg"))
                    {
                        goodsInfo.buyimage = ImageUtil.StrToUri(goodsInfo.buyimage, goodsInfo.ID + "_buy.jpg");
                    }
                    result.data = await this.Update(goodsInfo);
                    if (result.data)
                    {
                        result.code = 1;
                    }
                    else
                    {
                        result.code = -103;
                        result.message = "商品信息更新失败！";
                    }
                    return result;
                }
            })
           .WithLog("保存商品信息.")
           .Execute();
        }

        public async Task<ReturnResult<bool>> AuditGoodsInfo(GoodsModel goodsInfo)
        {
            return await Aspect.Task(async () =>
            {
                ReturnResult<bool> result = new ReturnResult<bool>();
                goodsInfo.audittime = DateTime.Now;
                result.data = await this.Update(goodsInfo);
                if (result.data)
                {
                    result.code = 1;
                }
                else
                {
                    result.code = -103;
                    result.message = "商品审核失败！";
                }
                return result;
            })
           .WithLog("审核商品信息.")
           .Execute();
        }

        /// <summary>
        /// 通过商品ID获取商品详细信息
        /// </summary>
        /// <param name="id">商品主键</param>
        /// <returns></returns>
        public async Task<ReturnResult<GoodsModel>> GetGoodsInfo(string id)
        {
            return await Aspect.Task<ReturnResult<GoodsModel>>(async () =>
            {
                ReturnResult<GoodsModel> result = new ReturnResult<GoodsModel>();

                GoodsModel goods = await this.GetOne(p => p.ID == id);

                if (goods == null || string.IsNullOrEmpty(goods.ID))
                {
                    result.code = -105;
                    result.message = "找不到指定商品。";
                    return result;
                }
                result.code = 1;
                result.data = goods;
                return result;
            })
            .WithLog("获取商品列表")
            .Execute();
        }

        /// <summary>
        /// 获取最近三天内的十条管理员推荐的商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnResult<PagedData<GoodsModel>>> GetRecentGoodsList()
        {
            int curPage = 1;
            int pageSize = 10;

            //获取三天内的数据
            DateTime today = DateTime.Now;
            DateTime startDate = DateTime.Now.AddDays(-3);

            return await Aspect.Task<ReturnResult<PagedData<GoodsModel>>>(async () =>
            {
                ReturnResult<PagedData<GoodsModel>> result = new ReturnResult<PagedData<GoodsModel>>();


                Expression<Func<Goods, bool>> exp = x => x.recommender == "230feefe-8ff6-46b7-883e-1da120a51139" &&
                    x.recommendtime >= startDate && x.recommendtime <= today &&
                    x.state == 2;

                var goods = await this.FindResultWithPagingAsync<GoodsModel, Goods>(repository, curPage, pageSize, exp,
                    x => new { x.recommendtime }, SortOrder.Descending);

                if (goods == null || goods.DataList == null || goods.DataList.Count == 0)
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
    }
}
