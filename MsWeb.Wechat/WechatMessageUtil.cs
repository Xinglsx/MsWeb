using MsWeb.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Xml;

namespace MsWeb.Wechat
{
    public class WechatMessageUtil
    {
        //private readonly string testToken = "mskj2017";//与微信公众账号后台的Token设置保持一致，区分大小写。
        //private readonly string testAppID = "wx28547fc8ad9232ba";
        //private readonly string testSecret = "aabe4685d9b918ceae7ef07501096ca5";
        private static readonly string sToken = "mskj2017";//与微信公众账号后台的Token设置保持一致，区分大小写。
        private static readonly string sAppID = "wx231cad5b531e5d35";
        private static readonly string sEncodingAESKey = "5gMwyIaHqwbJwGgoiwpu88ltq87M1wi1eNbTm9btnAJ";

        public static bool CheckToken(string signature, string timestamp, string nonce, string echostr)
        {
            string[] ArrTmp = { sToken, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            var result = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();

            if (signature.Equals(result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ProcessMessage(string receiveMessage)
        {
            string revertMessage = string.Empty;
            WechatXMLModel msgModel = new WechatXMLModel();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(receiveMessage);//读取xml字符串
                XmlElement root = doc.DocumentElement;
                msgModel.MsgType = root.SelectSingleNode("MsgType").InnerText;//消息类型
                msgModel.ToUserName = root.SelectSingleNode("ToUserName").InnerText;//本公众账号
                msgModel.FromUserName = root.SelectSingleNode("FromUserName").InnerText;//用户
                msgModel.CreateTime = root.SelectSingleNode("CreateTime").InnerText;//创建时间
                //需要考虑消息排重机制
                switch (msgModel.MsgType)
                {
                    case "text":
                        msgModel.Content = root.SelectSingleNode("Content").InnerText;//消息内容
                        revertMessage = GetTextResponseMessage(msgModel);
                        break;
                    case "image":
                        msgModel.PicUrl = root.SelectSingleNode("PicUrl").InnerText;
                        msgModel.MediaId = root.SelectSingleNode("MediaId").InnerText;
                        revertMessage = GetImageResponseMessage(msgModel);
                        break;
                    case "voice":
                        msgModel.MediaId = root.SelectSingleNode("MediaId").InnerText;
                        msgModel.Format = root.SelectSingleNode("Format").InnerText;
                        revertMessage = GetVoiceResponseMessage(msgModel);
                        break;
                    case "vedio":
                        msgModel.MediaId = root.SelectSingleNode("MediaId").InnerText;
                        msgModel.ThumbMediaId = root.SelectSingleNode("ThumbMediaId").InnerText;
                        revertMessage = GetVedioResponseMessage(msgModel);
                        break;
                    case "location":
                        msgModel.Location_X = root.SelectSingleNode("Location_X").InnerText;
                        msgModel.Location_Y = root.SelectSingleNode("Location_Y").InnerText;
                        msgModel.Scale = root.SelectSingleNode("Scale").InnerText;
                        msgModel.Label = root.SelectSingleNode("Label").InnerText;
                        revertMessage = GetLocationResponseMessage(msgModel);
                        break;
                    case "link":
                        msgModel.Title = root.SelectSingleNode("Title").InnerText;
                        msgModel.Description = root.SelectSingleNode("Description").InnerText;
                        msgModel.Url = root.SelectSingleNode("Url").InnerText;
                        revertMessage = GetLinkResponseMessage(msgModel);
                        break;
                    case "event":
                        msgModel.Event = root.SelectSingleNode("Event").InnerText;
                        //判断是否是扫描二维码的。如果是扫描二维码的，会存在已关注和未关注两种事件推送！
                        XmlNode temp = root.SelectSingleNode("EventKey");
                        if(temp != null)
                        {
                            msgModel.EventKey = temp.InnerText;
                            msgModel.Ticket = root.SelectSingleNode("Ticket").InnerText;
                            revertMessage = GetEventSubscribeResponseMessage(msgModel, true);
                        }
                        else
                        {
                            revertMessage = GetEventSubscribeResponseMessage(msgModel, false);
                        }
                        break;
                    default:
                        msgModel.Content = "未知消息，请等待客服处理，谢谢！";//消息内容
                        revertMessage = CreateResultXML(msgModel, "text");
                        break;
                }

            }
            catch (Exception exp)
            {
                LogUtil.WebError(exp);
                msgModel.Content = "对不起，服务器出了点故障，程序猿们正在紧张的处理之中，请您谅解！";//消息内容
                revertMessage = CreateResultXML(msgModel, "text");
            }
            return revertMessage;
        }
        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        public static string GetTextResponseMessage(WechatXMLModel msgModel)
        {
            string content = msgModel.Content;
            if (content == "帮助")
            {
                msgModel.Content = "目前实现功能有：\n1、文字消息处理\n2、发送位置，获取经纬度\n3、发送收藏的文章做简单解析\n经常来看看，可能会有意外收获哦！";
            }
            else
            {
                msgModel.Content = "您的消息，客服已经收到，稍后联系您！";
            }
            //回复消息的类型以及具体内容应该根据后台数据库获取到的数据和类型来组成。
            return CreateResultXML(msgModel,"text");
        }
        /// <summary>
        /// 图片消息处理
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        private static string GetImageResponseMessage(WechatXMLModel msgModel)
        {
            msgModel.Content = "您的图片已经收到，点击链接可以查看:"+msgModel.PicUrl;
            //回复消息的类型以及具体内容应该根据后台数据库获取到的数据和类型来组成。
            return CreateResultXML(msgModel, "text");
        }
        /// <summary>
        /// 语音消息处理
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        private static string GetVoiceResponseMessage(WechatXMLModel msgModel)
        {
            msgModel.Content = "语音识别功能暂未开放，如有需要，请等待客服回复您！";
            //回复消息的类型以及具体内容应该根据后台数据库获取到的数据和类型来组成。
            return CreateResultXML(msgModel, "news");
        }
        /// <summary>
        /// 视频消息处理
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        private static string GetVedioResponseMessage(WechatXMLModel msgModel)
        {
            msgModel.Content = "视频无法解析，如有需要，请等待客服回复您！";
            //回复消息的类型以及具体内容应该根据后台数据库获取到的数据和类型来组成。
            return CreateResultXML(msgModel, "text");
        }
        /// <summary>
        /// 地理位置消息处理
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        private static string GetLocationResponseMessage(WechatXMLModel msgModel)
        {
            //< Location_X > 31.816099 </ Location_X >
            //< Location_Y > 117.198387 </ Location_Y >
            //< Scale > 16 </ Scale >
            //< Label >< ![CDATA[安徽省合肥市蜀山区科学大道7号]] ></ Label >
            msgModel.Content = string.Format("您现在所在的\n经度：{0}\n纬度：{1}\n所在地址：{2}", msgModel.Location_Y,
                msgModel.Location_X,msgModel.Label);
            if (msgModel.Label.Contains("合肥"))
            {
                msgModel.Content += "\n原来你也在合肥呀！真巧！真巧！";
            }
            //回复消息的类型以及具体内容应该根据后台数据库获取到的数据和类型来组成。
            return CreateResultXML(msgModel, "text");
        }
        /// <summary>
        /// 微信官方图文消息处理
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        private static string GetLinkResponseMessage(WechatXMLModel msgModel)
        {
            //< Title >< ![CDATA[史上最全29个自我管理工具，成功人士都在用！]] ></ Title >
            //< Description >< ![CDATA[史上最全29个自我管理工具，成功人士都在用！]] ></ Description >
            //< Url >< ![CDATA[http://mp.weixin.qq.com/s?__biz=MzIyNjc2MzM2NA==&mid=2247486847&idx=4&sn=e0ebe51b3dfe5de939370549c6557866&chksm=e86a3a74df1db362787df9268f68c86ea29861569efbeef1a421b375cfab83ce15c4b5f6f820&scene=0#rd]]></Url>
            msgModel.Content = string.Format("文章已解析成功\n文章题目：{0}\n文章概述：{1}\n文章链接：{2}。",
                msgModel.Title,msgModel.Description,msgModel.Url);
            //回复消息的类型以及具体内容应该根据后台数据库获取到的数据和类型来组成。
            return CreateResultXML(msgModel, "text");
        }
        /// <summary>
        /// 订阅、取消订阅事件处理
        /// </summary>
        /// <param name="msgModel"></param>
        /// <param name="isQrcode">是否是扫描特殊的二维码</param>
        /// <returns></returns>
        public static string GetEventSubscribeResponseMessage(WechatXMLModel msgModel,bool isQrcode)
        {
            //首次关注提醒的消息
            string subscribeMessage = "感谢您关注合肥MSKJ。科技引领未来，应用改变现在！目前我们提供的服务主要有："
                        + "\n1、计算机软件开发"
                        + "\n2、安卓手机APP开发"
                        + "\n3、公司宣传主页开发"
                        + "\n4、产品宣传主页开发"
                        + "\n5、网站SEO"
                        + "\n6、计算机及周边硬件销售"
                        + "\n7、微信公众号开发"
                        + "\n8、软件培训等等。"
                        + "\n回复数字可以查看详细介绍！";
            if (isQrcode)
            {
                if (msgModel.EventKey.StartsWith("qrscene_"))
                {
                    msgModel.Content = subscribeMessage;
                }
                else
                {
                    msgModel.Content = "您已经通过"+msgModel.EventKey+"关注了公众账号！";
                }
            }
            else
            {
                string content = msgModel.Content;
                if ("subscribe".Equals(msgModel.Event))
                {
                    msgModel.Content = subscribeMessage;
                }
                else if ("unsubscribe".Equals(msgModel.Event))
                {
                    msgModel.Content = "感谢您这么长时间的订阅，期待您的重新归来！";
                }
                else
                {
                    msgModel.Content = "事件消息好像出了点小问题！";
                }
            }
            //回复消息应该根据后台数据库获取到的数据和类型来组成。
            return CreateResultXML(msgModel, "text");
        }
        /// <summary>
        /// 创建返回的XML
        /// </summary>
        /// <param name="msgModel">消息对象</param>
        /// <param name="backMsgType">回传消息类型</param>
        /// <returns></returns>
        public static string CreateResultXML(WechatXMLModel msgModel,string backMsgType)
        {
            string resxml = string.Empty;
            resxml = string.Format("<xml>");
            resxml += string.Format("<ToUserName><![CDATA[{0}]]></ToUserName>", msgModel.FromUserName);
            resxml += string.Format("<FromUserName><![CDATA[{0}]]></FromUserName>", msgModel.ToUserName);
            resxml += string.Format("<CreateTime>{0}</CreateTime>", DateTimeUtil.DateTimeToUnixInt(DateTime.Now));
            switch (backMsgType)
            {
                //回复图文消息
                case "news":
                    int articleCount = 2;//支持最多8条图文，默认第一个item为大图
                    resxml += string.Format("<MsgType><![CDATA[news]]></MsgType>");
                    resxml += string.Format("<ArticleCount>{0}</ArticleCount>",articleCount);
                    resxml += string.Format("<Articles>");
                    for (int i = 0; i < articleCount;i++)
                    {
                        resxml += string.Format("<item>");
                        resxml += string.Format("<Title><![CDATA[{0}]]></Title>","这是一条测试图文");
                        resxml += string.Format("<Description><![CDATA[{0}]]></Description>", "这里就是测试图文的文章简单描述，不知道最多可以支持多少个字呢！");
                        resxml += string.Format("<PicUrl><![CDATA[{0}]]></PicUrl>", "http://mmbiz.qpic.cn/mmbiz_jpg/1Z80LXZ2oJnc6Cq0GkbiaMdBzssft7ZwPpeq0r74DAcNcWvayxATPEufnsrZyiaF9iazQKHfjGumtBWQmxec7awYQ/0");
                        resxml += string.Format("<Url><![CDATA[{0}]]></Url>", "http://mp.weixin.qq.com/s/m6cERzfeti3cuhloGm9YZg");
                        resxml += string.Format("</item>");
                    }
                    resxml += string.Format("</Articles>");
                    break;
                //默认回复文本消息
                default:
                    resxml += string.Format("<MsgType><![CDATA[text]]></MsgType>");
                    resxml += string.Format("<Content><![CDATA[{0}]]></Content>",msgModel.Content);
                    resxml += string.Format("<FuncFlag>0</FuncFlag>");
                    break;
            }
            resxml += string.Format("</xml>");
            LogUtil.WebLog("订阅号回送消息:" + resxml);
            return resxml;

        }
    }
}
