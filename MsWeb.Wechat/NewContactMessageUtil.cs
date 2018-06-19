using MsWeb.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.MP.Containers;
using System;

namespace MsWeb.Wechat
{
    public class NewContactMessageUtil
    {
        public static string appid = "wx8773baee612e9144";
        public static string secret = "c7254c2be73a60f7e9408ebb91735c53";

        public static string GetUserOpenId()
        {
            string openid = string.Empty;
            openid = WechatHttpClientUtil.dooGet(
                   string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid=",
                   AccessTokenContainer.TryGetAccessToken(appid, secret)));
            return openid;
            
        }
        /// <summary>
        /// 发送模板消息给用户（测试账号已通）
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="template_id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool SendMessageToUser(string openid, string template_id,string targetUrl, string data)
        {
            bool isSuccess = true;
            string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}",
                AccessTokenContainer.TryGetAccessToken(appid, secret));
            //var result = ;
            LogUtil.WebLog("postUrl:" + postUrl);
            //{{User.DATA}}您的订单{{Goods.DATA}}已发送成功，点击可查看订单详情！
            //模板ID：PwSL3zRR1BvqeV74VCHVeRrKr_VVqpHoVtMsetUB65c
            //openid:ogXxw0uil2siqIvGLne_ATt0SufI
            /*
             {
                "touser": "o-HDs1CyHSDOM8-XvMe0g4mvXHio", 
                "template_id": "seqcZG4-OgC_OkJkPctcfE9EC7kGuVbZpvCf4AaXm_s", 
                "url": "http://weixin.qq.com/download", 
                "topcolor": "#FF0000", 
                "data": {
                    "User": {
                        "value": "李先生", 
                        "color": "#173177"
                    }, 
                    "Date": {
                        "value": "06月07日 19时24分", 
                        "color": "#173177"
                    }, 
                    "Goods": {
                        "value": "图书", 
                        "color": "#173177"
                    }
                }
            }
             */
            try
            {
                //需要传入
                if (string.IsNullOrEmpty(openid))
                {
                    openid = "ogXxw0uil2siqIvGLne_ATt0SufI";
                }
                if (string.IsNullOrEmpty(template_id))
                {
                    //template_id = "PwSL3zRR1BvqeV74VCHVeRrKr_VVqpHoVtMsetUB65c";
                    //template_id = "YP1LIRbJaKHoIDffsQXrYLTSjLouD3IHgXVJ5SK-sOM";
                    template_id = "X8SSQnupBSVaz5Lruw0Iq813Sj9NsKdDtZRpYB2MVbc";
                }
                if (string.IsNullOrEmpty(targetUrl))
                {
                    targetUrl = "http://www.baidu.com";
                }
                data = "{\"User\": {\"value\":\"匠心立榜首\",\"color\": \"#173177\"},"
                    + "\"Goods\":{\"value\":\"【天堂雨伞】\",\"color\": \"#FF0000\"},"
                    + "\"Date\":{\"value\":\"2018年4月20日上午10：00\",\"color\": \"#173177\"},"
                    + "\"Instructions\":{\"value\":\"点击可查看详情！\",\"color\": \"#173177\"},"
                    + "\"Prize\":{\"value\":\"苹果笔记本电脑，Iphone X\",\"color\":\"#FF0000\"}}";
                LogUtil.WebLog("data:" + data);
                LogUtil.WebLog(string.Format("\"touser\":\"{0}\",", openid));
                string postData ="{" + string.Format("\"touser\":\"{0}\",", openid)
                    + string.Format("\"template_id\":\"{0}\",", template_id)
                    + string.Format("\"url\":\"{0}\",", targetUrl)
                    + "\"color\":\"#FF0000\","
                    + string.Format("\"data\":{0}", data)
                    +"}";
                LogUtil.WebLog("postData:" + postData);
                string jsonResult = WechatHttpClientUtil.dooPost(postUrl, postData, "json");
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonResult);
                if (jo["errcode"].ToString() != "0")
                {
                    isSuccess = false;
                }
            }
            catch (Exception exp)
            {
                isSuccess = false;
                LogUtil.WebError(exp);
            }
            
            return isSuccess;
        }
    }
}
