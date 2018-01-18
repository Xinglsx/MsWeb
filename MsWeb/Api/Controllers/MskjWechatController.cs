using MsWeb.Core.Utils;
using MsWeb.DataObjects;
using MsWeb.Wechat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;

namespace MsWeb.Api.Controllers
{
    public class MskjWechatController : Controller
    {
        [HttpGet]
        public void Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (WechatMessageUtil.CheckToken(signature, timestamp, nonce, echostr))
            {
                Response.Write(echostr);
                Response.End();
            }
            else
            {
                Response.Write("Error!");
                Response.End();
            }
        }
        [HttpPost]
        public void Get()
        {
            string weixin = "";//获取xml数据
            weixin = PostInput();//自定义方法，获取xml数据
            LogUtil.WebLog("用户发送消息:" + weixin);
            Response.Write(WechatMessageUtil.ProcessMessage(weixin));
            Response.End();
        }

        private string PostInput()/// 获取post请求数据
        {
            Stream s = Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);
        }

        [HttpGet]
        public string CreateMenu()
        {
            return MskjMenuUtil.CreateMenu();
        }
    }

}