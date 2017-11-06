using Baidu.Aip.ImageClassify;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MsWeb.BaiduAI
{
    public class ImageClassifyUtil
    {
        private static ImageClassify client = new ImageClassify(API_KEY, SECRET_KEY);

        static string APP_ID = "10320070";
        static string API_KEY = "ok116culHGa4axGNrVNyfA2k";
        static string SECRET_KEY = "V0r4WoD8M0SXGaBmZxMkPuZCplvCDCXn ";

        public static string DishDetect()
        {
            var image = File.ReadAllBytes("D:\\Code\\Android\\MsWeb\\MsWeb\\images\\car\\car1.jpg");
            JObject result = new JObject();
            // 调用车辆识别
            var options = new Dictionary<string, object>
            {
                {"top_num", 3}
            };
            // 带参数调用车辆识别
            result = client.CarDetect(image, options);
            return result.ToString();
        }
    }
}