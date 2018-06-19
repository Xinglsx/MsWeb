using MsWeb.Core.Utils;
using MsWeb.DataObjects;
using MsWeb.Wechat;
using MsWeb.Wechat.CustomMessageHandler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Request;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace MsWeb.Api.Controllers
{
    public class NewContactWechatController : Controller
    {
        /*
         * 正式账号账号
        //与微信公众账号后台的Token设置保持一致，区分大小写。
        private static readonly string sToken = "NewContact";
        private static readonly string sAppID = "wx7d7efe9593cc9c7b";
        private static readonly string sEncodingAESKey = "BjVP6PnzO7GYoEZfsGInVj6mlgu2Pij6w8wLVGr32FI";
         */
        //测试账号
        private static readonly string sToken = "NewContact";
        private static readonly string sAppID = "wx8773baee612e9144";
        private static readonly string sEncodingAESKey = "c7254c2be73a60f7e9408ebb91735c53";
        [HttpGet]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, sToken))
            {
                return Content(echostr);
            }
            else
            {
                return Content("Error!");
            }
        }
        [HttpPost]
        [ActionName("Get")]
        public ActionResult Post(PostModel postModel)
        {
            string weixin = "";//获取xml数据
            weixin = PostInput();//自定义方法，获取xml数据
            LogUtil.WebLog("用户发送消息:" + weixin);

            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, sToken))
            {
                LogUtil.WebLog("参数错误!");
                return Content("参数错误！");
            }

            postModel.Token = sToken;//根据自己后台的设置保持一致
            postModel.EncodingAESKey = sEncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = sAppID;//根据自己后台的设置保持一致

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);//接收消息

            messageHandler.Execute();//执行微信处理过程

            return new FixWeixinBugWeixinResult(messageHandler);//返回结果
        }

        //[HttpPost]
        //public void Get()
        //{
        //    string weixin = "";//获取xml数据
        //    weixin = PostInput();//自定义方法，获取xml数据
        //    LogUtil.WebLog("用户发送消息:" + weixin);
        //    Response.Write("Test is OK");
        //    Response.End();
        //}

        private string PostInput()/// 获取post请求数据
        {
            Stream s = Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);
        }
        [HttpGet]
        public bool GetSendMessage(string openId,string template_id,string targetUrl)
        {
            LogUtil.WebLog("开始发送消息!");
            bool result = NewContactMessageUtil.SendMessageToUser(openId, template_id, targetUrl, "");
            if (result)
            {
                LogUtil.WebLog("消息发送成功!");
            }
            else
            {
                LogUtil.WebLog("消息发送失败!");
            }
            return result;
        }
        [HttpGet]
        public string GetUserOpenId()
        {
            return NewContactMessageUtil.GetUserOpenId();
        }

        [HttpGet]
        public string CreateMenu()
        {
            return NewContactMenuUtil.CreateMenu();
        }
        /// <summary>
        /// 绑定用户信息与微信OPENID，方便后面给指定用户发消息时绑定到用户的微信
        /// </summary>
        /// <param name="wechatCode">授权返回的code</param>
        [HttpGet]
        public ActionResult GetAuthoriseState(string wechatCode)
        {
            LogUtil.WebLog("wechatCode:"+ wechatCode);
            ReturnResult<UserInfo> result = new ReturnResult<UserInfo>();
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                sAppID, sEncodingAESKey, wechatCode);
            string jsonResult = WechatHttpClientUtil.dooGet(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonResult);
            //LogUtil.WebLog(jo);
            //{
            //    "access_token":"ACCESS_TOKEN",
            //    "expires_in":7200,
            //    "refresh_token":"REFRESH_TOKEN",
            //    "openid":"OPENID",
            //    "scope":"SCOPE" 
            //}
            string openId = jo["openid"].ToString();
            LogUtil.WebLog("openid：" + openId);
            string access_token = jo["access_token"].ToString();
            LogUtil.WebLog("access_token：" + access_token);
            string refresh_token = jo["refresh_token"].ToString();
            LogUtil.WebLog("refresh_token：" + refresh_token);
            string TryGetAccessToken = AccessTokenContainer.TryGetAccessToken(sAppID, sEncodingAESKey);
            LogUtil.WebLog("TryGetAccessToken：" + TryGetAccessToken);
            //string refresh_token = jo["refresh_token"].ToString();
            //string access_token = jo["access_token"].ToString();
            /*通过业务接口判断openid是否已绑定用户；如果已经绑定，返回用户信息，无需用户再登录；
             *如果未绑定，则留在绑定界面进行绑定数据
             */
            Random rd = new Random();
            if ( rd.Next(10) > 5)
            {
                result.code = -1;
                result.data = null;
                result.message = "该用户未绑定！";
            }
            else
            {
                result.code = 1;
                result.data = new UserInfo
                {
                    ID = "01c90cf1-084e-4444-b78a-ccb8b44d34be",
                    userCode = "Lisx24",
                    userName = "舒适明天",
                };
                result.message = "该用户已经绑定！openid为："+ openId;
            }
            string urlUserinfo = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN",
                TryGetAccessToken, openId);
            string jsonResultUserinfo = WechatHttpClientUtil.dooGet(urlUserinfo);
            JObject joUserinfo = (JObject)JsonConvert.DeserializeObject(jsonResultUserinfo);
            //{
            //    "subscribe": 1, 
            //    "openid": "o6_bmjrPTlm6_2sgVt7hMZOPfL2M", 
            //    "nickname": "Band", 
            //    "sex": 1, 
            //    "language": "zh_CN", 
            //    "city": "广州", 
            //    "province": "广东", 
            //    "country": "中国", 
            //    "headimgurl":"http://thirdwx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0",
            //    "subscribe_time": 1382694957,
            //    "unionid": " o6_bmasdasdsad6_2sgVt7hMZOPfL"
            //    "remark": "",
            //    "groupid": 0,
            //    "tagid_list":[128,2]
            //}
            LogUtil.WebLog("获取到的用户信息为："+joUserinfo.ToString());
            LogUtil.WebLog("方法调用成功！"+result.message);
            return  this.Json(result,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 绑定用户信息与微信OPENID，方便后面给指定用户发消息时绑定到用户的微信
        /// </summary>
        /// <param name="wechatCode">授权返回的code</param>
        /// <param name="userCode">用户code</param>
        [HttpPost]
        public string AuthoriseUserOpenId(string wechatCode,string usercode,string password)
        {
            //需要再次去获取openid
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                sAppID, sEncodingAESKey, wechatCode);
            string jsonResult = WechatHttpClientUtil.dooGet(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonResult);
            //{
            //    "access_token":"ACCESS_TOKEN",
            //    "expires_in":7200,
            //    "refresh_token":"REFRESH_TOKEN",
            //    "openid":"OPENID",
            //    "scope":"SCOPE" 
            //}
            string openId = jo["openid"].ToString();
            LogUtil.WebLog("openid：" + openId);
            string refresh_token = jo["refresh_token"].ToString();
            string access_token = jo["access_token"].ToString();
            /*将openid\usercode\password直接传入业务接口进行验证，验证成功后进行绑定，并将关系保存在后台。
             */
            LogUtil.WebLog(jo);
            return "success";
        }

    }

}