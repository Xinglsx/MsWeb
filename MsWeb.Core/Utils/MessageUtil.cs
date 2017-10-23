using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MsWeb.Core.Utils
{
    public class MessageUtil
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        public static string SendMessageVerificationCode(string mobileNo)
        {
            // 设置为您的apikey(https://www.yunpian.com)可查
            string apikey = "83d731ef2e511e863b8944427ef3e38e";
            // 发送的手机号
            string mobile = mobileNo;
            // 发送模板编号
            int tpl_id = 1;
            // 发送模板内容
            mobile = HttpUtility.UrlEncode(mobile, Encoding.UTF8);
            string tpl_value = HttpUtility.UrlEncode(
            HttpUtility.UrlEncode("#code#", Encoding.UTF8) + "=" +
            HttpUtility.UrlEncode("1234", Encoding.UTF8) + "&" +
            HttpUtility.UrlEncode("#company#", Encoding.UTF8) + "=" +
            HttpUtility.UrlEncode("云片网", Encoding.UTF8), Encoding.UTF8);
            // 发送内容
            string text = "【闪荐网】您的验证码是#code#。如非本人操作，请忽略本短信;";
            //// 获取user信息url
            //string url_get_user = "https://sms.yunpian.com/v2/user/get.json";
            // 智能模板发送短信url
            string url_send_sms = "https://sms.yunpian.com/v2/sms/single_send.json";
            //// 指定模板发送短信url
            //string url_tpl_sms =
            //    "https://sms.yunpian.com/v2/sms/tpl_single_send.json";
            //// 发送语音短信url
            //string url_send_voice = "https://voice.yunpian.com/v2/voice/send.json";

            //string data_get_user = "apikey=" + apikey;
            string data_send_sms = "apikey=" + apikey + "&mobile=" + mobile + "&text=" +
                text;
            //string data_tpl_sms = "apikey=" + apikey + "&mobile=" + mobile +
            //    "&tpl_id=" + tpl_id.ToString() + "&tpl_value=" + tpl_value;
            //string data_send_voice = "apikey=" + apikey + "&mobile=" + mobile +
            //    "&code=" + "1234";

            //HttpPost(url_get_user, data_get_user);
            HttpPost(url_send_sms, data_send_sms);
            //HttpPost(url_tpl_sms, data_tpl_sms);
            //HttpPost(url_send_voice, data_send_voice);

            return "1234";
        }

        public static void HttpPost(string Url, string postDataStr)
        {
            byte[] dataArray = Encoding.UTF8.GetBytes(postDataStr);
            // Console.Write(Encoding.UTF8.GetString(dataArray));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataArray.Length;
            //request.CookieContainer = cookie;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader =
                    new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                String res = reader.ReadToEnd();
                reader.Close();
                Console.Write("\nResponse Content:\n" + res + "\n");
            }
            catch (Exception e)
            {
                Console.Write(e.Message + e.ToString());
            }
        }
    }
}