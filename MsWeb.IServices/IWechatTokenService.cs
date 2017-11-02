using Mingshu.Framework.MSWeb.Services;
using Mingshu.Framework.MSWeb.Services.ServiceContract;
using MsWeb.DataObjects;
using MsWeb.DataObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsWeb.IServices
{
    public interface IWechatTokenService : IBaseServiceKeyedString<WechatTokenModel>, IServiceContract
    {
        /// <summary>
        /// 获取本地已存在的微信Token,用于后续数据接口
        /// </summary>
        /// <returns></returns>
        Task<ReturnResult<WechatTokenModel>> GetWechatToken();
        /// <summary>
        /// 更新微信Token
        /// 微信Token有效时长为2小时，每天获取上限为2000次
        /// </summary>
        /// <param name="saveToken"></param>
        /// <returns></returns>
        Task<ReturnResult<bool>> UpdateWechatToken(WechatTokenModel saveToken);
    }
}
