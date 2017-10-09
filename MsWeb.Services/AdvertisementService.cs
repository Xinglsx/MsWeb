﻿using MsWeb.DataObjects;
using MsWeb.Domains;
using System;
using System.Threading.Tasks;
using Winning.Framework.DMSP.Services;
using Winning.Framework.DMSP.EFRepository;
using Winning.Framework.DMSP.Core.AspectX;
using System.Linq.Expressions;
using MsWeb.IServices;

namespace MsWeb.Services
{
    public class AdvertisementService : BaseServiceKeyedString<AdvertisementsModel, Advertisements>, IAdvertisementService
    {
        public AdvertisementService(IRepositoryContext context, IRepository<Advertisements> repository) : base(context, repository)
        {
        }

        public async Task<ReturnResult<AdvertisementsModel>> GetAdvertisement(string key)
        {
            return await Aspect.Task(async () =>
            {
                ReturnResult<AdvertisementsModel> result = new ReturnResult<AdvertisementsModel>();
                Expression<Func<Advertisements, bool>> exp = x => x.key == key;
                var adTemp = await this.FindOneAsync<AdvertisementsModel, Advertisements>(repository, exp);
                if (adTemp == null || string.IsNullOrEmpty(adTemp.ID))
                {
                    result.code = -104;
                    result.message = "未查询到广告数据。";
                    return result;
                }
                result.code = 1;
                result.data = adTemp;
                return result;
            })
            .WithLog("获取广告信息.")
            .Execute();
        }
    }
}