using MsWeb.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MsWeb.Wechat
{
    public class WechatHttpClientUtil
    {
        /// <summary>
        /// HttpClient实现Post请求(异步)  
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="bodyData">post的数据</param>
        /// <param name="bodyType">post数据类型:xml json</param>
        public static string dooPost(string url,string bodyData,string bodyType)
        {
            string strURL = url;
            HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            if ("json".Equals(bodyType))
            {
                request.ContentType = "application/json;charset=UTF-8";
            }
            else
            {
                request.ContentType = "application/xml;charset=UTF-8";
            }
            string paraUrlCoded = bodyData;
            byte[] payload;
            payload = Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }
        /// <summary>
        /// HttpClient实现Get请求(异步)
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="bodyData">post的数据</param>
        /// <param name="bodyType">post数据类型:xml json</param>
        public static string dooGet(string url, string bodyData = "", string bodyType = "")
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (var http = new HttpClient(handler))
            {
                var response = http.GetAsync(url);
                string result = response.Result.Content.ReadAsStringAsync().Result;
                LogUtil.WebLog(result);
                return result;
            }
        }
    }
}
