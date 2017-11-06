using Baidu.Aip.Ocr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MsWeb.BaiduAI
{
    public class WordOcrUtil
    {
        static string APP_ID = "10320066";
        static string API_KEY = "UKPRuLV0qaVkHkjREq5V19PL";
        static string SECRET_KEY = "xuYUNYjTupPPq1u7PTGwk05lYLmhFAVn";

        public static string Cpzcz()
        {
            var client = new Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes("D:\\Code\\Android\\MsWeb\\MsWeb\\images\\ocr\\text1.jpg");
            // 通用文字识别
            // 过程中发生的网络失败等系统错误，将会抛出相关异常，请使用 try/catch 捕获。
            var result = client.GeneralBasic(image, null);
            return result.ToString();
        }
    }
}
