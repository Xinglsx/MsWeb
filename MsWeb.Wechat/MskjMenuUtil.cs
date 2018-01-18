using Mingshu.Framework.MSWeb.Core.AspectX;
using Mingshu.Framework.MSWeb.EFRepository;
using MsWeb.Core.Utils;
using MsWeb.DataObjects;
using MsWeb.DataObjects.Model;
using MsWeb.Domains;
using MsWeb.IServices;
using MsWeb.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsWeb.Wechat
{
    public class MskjMenuUtil
    {
        public static string appid = "wx28547fc8ad9232ba";
        public static string secret = "aabe4685d9b918ceae7ef07501096ca5";

        public static string CreateMenu()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appid, secret);
            ButtonGroup bg = new ButtonGroup();
            //单击
            bg.button.Add(new SingleViewButton()
            {
                url = "http://www.mingshukeji.com.cn/SharePing",
                name = "小米小店"
            });
            //二级菜单
            var subButton = new SubButton()
            {
                name = "抢券中心"
            };
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "http://www.mingshukeji.com.cn/SharePing",
                name = "推荐福利"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "http://www.mingshukeji.com.cn/SharePing/FindCoupon",
                name = "搜索福利"
            });
            bg.button.Add(subButton);

            //二级菜单
            var subButton2 = new SubButton()
            {
                name = "服务中心"
            };
            subButton2.sub_button.Add(new SingleViewButton()
            {
                url = "https://www.mingshukeji.com.cn/Home/AboutCompany",
                name = "关于我们"
            });
            subButton2.sub_button.Add(new SingleViewButton()
            {
                url = "https://shop.mi.com",
                name = "管理小店"
            });
            bg.button.Add(subButton2);

            var result = CommonApi.CreateMenu(accessToken, bg);
            return "菜单创建成功";
        }
    }
}
