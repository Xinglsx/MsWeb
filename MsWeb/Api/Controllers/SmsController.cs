using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using MsWeb.Core.Utils;
using MsWeb.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MsWeb.Api.Controllers
{
    public class SmsController : Controller
    {
        // GET: Sms
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 短信发送平台
        /// </summary>
        [HttpGet]
        public ReturnResult<bool> GetSendMessage(string phoneNumber,string code)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            String product = "Dysmsapi";//短信API产品名称
            String domain = "dysmsapi.aliyuncs.com";//短信API产品域名
            String accessKeyId = "LTAIoae9O4UxOJIG";//你的accessKeyId
            String accessKeySecret = "qtFsyoEWYpobLbSoC8TzWiyjvp6vhK";//你的accessKeySecret

            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
            //IAcsClient client = new DefaultAcsClient(profile);
            // SingleSendSmsRequest request = new SingleSendSmsRequest();

            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为20个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = phoneNumber;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = "闪荐";
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = "SMS_110400008";
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                request.TemplateParam = "{\"code\":\""+ code + "\"}";
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                request.OutId = "21212121211";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                LogUtil.WebLog(sendSmsResponse.Message);
                result.code = 1;
                result.data = true;
            }
            catch (ServerException e)
            {
                LogUtil.WebError(e);
                result.code = -1;
                result.message = e.Message;
            }
            catch (ClientException e)
            {
                LogUtil.WebError(e);
                result.code = -1;
                result.message = e.Message;
            }
            return result;
        }
    }
}