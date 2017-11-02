using Mingshu.Framework.MSWeb.Services;
using MsWeb.DataObjects.Model;
using MsWeb.Domains;
using MsWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mingshu.Framework.MSWeb.EFRepository;
using MsWeb.DataObjects;
using Mingshu.Framework.MSWeb.Core.AspectX;
using System.Linq.Expressions;

namespace MsWeb.Services
{
    public class WechatTokenService : BaseServiceKeyedString<WechatTokenModel, WechatToken>, IWechatTokenService
    {
        public WechatTokenService(IRepositoryContext context, IRepository<WechatToken> repository) : base(context, repository)
        {
        }

        /// <summary>
        /// 获取本地已存在的微信Token
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnResult<WechatTokenModel>> GetWechatToken()
        {
            return await Aspect.Task(async () =>
            {
                ReturnResult<WechatTokenModel> result = new ReturnResult<WechatTokenModel>();
                var token = await this.GetOne();
                result.code = 1;
                result.data = token;
                return result;
            })
           .WithLog("获取本地已存在的微信Token.")
           .Execute();
        }
        /// <summary>
        /// 更新微信Token
        /// 微信Token有效时长为2小时，每天获取上限为2000次
        /// </summary>
        /// <param name="saveToken">待更新的微信Token</param>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> UpdateWechatToken(WechatTokenModel saveToken)
        {
            return await Aspect.Task(async () =>
            {
                ReturnResult<bool> result = new ReturnResult<bool>();
                var token = await this.GetOne();
                //没有则新增
                if(token == null || string.IsNullOrEmpty(token.ID))
                {
                    result.data = await this.Add(saveToken);
                    if (result.data)
                    {
                        result.code = 1;
                    }
                    else
                    {
                        result.code = -106;
                        result.message = "未找到新增微信Token数据！";
                    }
                }
                else
                {
                    token.token = saveToken.token;
                    token.updatetime = DateTime.Now;
                    result.data = await this.Update(token);
                    if (result.data)
                    {
                        result.code = 1;
                    }
                    else
                    {
                        result.code = -103;
                        result.message = "微信Token数据更新失败！";
                    }
                }
                if (result.data)
                {
                    result.code = 1;
                }
                else
                {
                    result.code = -103;
                    result.message = "用户信息更新失败！";
                }
                return result;
            })
           .WithLog("更新用户信息.")
           .Execute();
        }
    }
}
