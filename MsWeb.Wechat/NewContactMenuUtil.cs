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
    public class NewContactMenuUtil
    {
        public static string appid = "wx8773baee612e9144";
        public static string secret = "c7254c2be73a60f7e9408ebb91735c53";

        public static string CreateMenu()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appid, secret);
            ButtonGroup bg = new ButtonGroup();
            //单击
            bg.button.Add(new SingleClickButton()
            {
                name = "单击测试",
                key = "OneClick",
                type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
            });

            //二级菜单
            var subButton = new SubButton()
            {
                name = "二级菜单"
            };
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Text",
                name = "返回文本"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_News",
                name = "返回图文"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Music",
                name = "返回音乐"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx8773baee612e9144&redirect_uri=http%3a%2f%2fwww.mingshukeji.com.cn%2fWechatManage%2fLoginAuthorise&response_type=code&scope=snsapi_userinfo&state=success#wechat_redirect",
                name = "我的账号"
            });
            bg.button.Add(subButton);

            var result = CommonApi.CreateMenu(accessToken, bg);
            return "菜单创建成功";
        }
    }
}
