using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;

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
            ////单击
            //bg.button.Add(new SingleClickButton()
            //{
            //    name = "单击测试",
            //    key = "OneClick",
            //    type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
            //});
            //bg.button.Add(new SingleViewButton()
            //{
            //    url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx8773baee612e9144&redirect_uri=http%3a%2f%2fwww.mingshukeji.com.cn%2fwechat%2fh5%2f%23%2fanswer&response_type=code&scope=snsapi_userinfo&state=success#wechat_redirect",
            //    name = "有奖问题"
            //});
            //二级菜单
            var subButton = new SubButton()
            {
                name = "公司介绍"
            };
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "https://www.baidu.com/",
                name = "联系我们"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "https://www.baidu.com/",
                name = "关于快享"
            });
            bg.button.Add(subButton);

            var subButton1 = new SubButton()
            {
                name = "方案介绍"
            };
            subButton1.sub_button.Add(new SingleViewButton()
            {
                url = "https://www.baidu.com/",
                name = "Q&A"
            });
            subButton1.sub_button.Add(new SingleViewButton()
            {
                url = "https://www.baidu.com/",
                name = "案例分享"
            });
            subButton1.sub_button.Add(new SingleViewButton()
            {
                url = "https://www.baidu.com/",
                name = "院内物流中心"
            });
            subButton1.sub_button.Add(new SingleViewButton()
            {
                url = "https://www.baidu.com/",
                name = "物资云"
            });
            bg.button.Add(subButton1);

            bg.button.Add(new SingleViewButton()
            {
                //url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx8773baee612e9144&redirect_uri=http%3a%2f%2fwww.mingshukeji.com.cn%2fWechatManage%2fLoginAuthorise&response_type=code&scope=snsapi_userinfo&state=success#wechat_redirect",
                url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx8773baee612e9144&redirect_uri=http%3a%2f%2fwww.mingshukeji.com.cn%2fQuickShareWechat%2fh5%2f%23%2fregister&response_type=code&scope=snsapi_userinfo&state=success#wechat_redirect",
                name = "有奖问答"
            });
            var result = CommonApi.CreateMenu(accessToken, bg);
            return "菜单创建成功";
        }
    }
}
