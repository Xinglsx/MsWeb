using MsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MsWeb.Controllers
{
    public class SharePingController : Controller
    {
        private List<HelpModel> Questions;

        public SharePingController()
        {
            CreateHelpModel();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "闪荐-寻找更便宜的好东西";
            return View();
        }

        public ActionResult FindCoupon()
        {
            ViewBag.Title = "闪荐-淘宝天猫优惠券搜索";
            return View("~/Views/SharePing/FindCoupon.cshtml");
        }

        public ActionResult UploadCoupon()
        {
            ViewBag.Title = "闪荐-上传分享商品";
            return View("~/Views/SharePing/UploadCoupon.cshtml");
        }
        public ActionResult AboutSharePing()
        {
            ViewBag.Title = "闪荐-关于闪荐";
            return View("~/Views/SharePing/AboutSharePing.cshtml");
        }

        public ActionResult Login()
        {
            ViewBag.Title = "闪荐-登陆";
            return View("~/Views/SharePing/Login.cshtml");
        }

        public ActionResult Register()
        {
            ViewBag.Title = "闪荐-注册";
            return View("~/Views/SharePing/Register.cshtml");
        }
        public ActionResult SjDownload()
        {
            ViewBag.Title = "闪荐-Android版本下载";
            return View("~/Views/SharePing/SjDownload.cshtml");
        }

        public ActionResult SoftDownload()
        {
            ViewBag.Title = "闪荐-软件下载中心";
            return View("~/Views/SharePing/SoftDownload.cshtml");
        }
        public ActionResult Help(String id)
        {
            List<HelpModel> temp = Questions.FindAll(p => p.ID == id);
            if (temp == null || temp.Count <= 0)
            {
                temp = Questions;
            }
            ViewBag.Title = "闪荐-帮助中心";
            return View(temp);
        }

        public void CreateHelpModel()
        {
            Questions = new List<HelpModel>();
            Questions.Add(new HelpModel
            {
                ID = "1",
                Question = "[闪荐]应用有什么作用?",
                Explain = @"[闪荐]，只为寻找更便宜的好东西；是一款专为用户提供淘宝、天猫优惠券的Android应用。在这里你可以看到其它用户推荐的商品和优惠券，也可以自己搜索优惠券！"
            });
            Questions.Add(new HelpModel
            {
                ID = "2",
                Question = "首页推荐商品都是谁发布的？",
                Explain = "其它真实用户发布。闪荐通过个人申请寻找到一些淘宝达人，他们将自己平时购买到的不错的东西，推荐到闪荐中，供大家参考选择。所有的推荐都真实有效，且通过人工一一审核，请放心选购！"
            });
            Questions.Add(new HelpModel
            {
                ID = "3",
                Question = "我也想推荐商品，如何操作呢？",
                Explain = "需要申请成为特约用户，软件推广阶段，1元即可成为特约用户，并提供相应的帮助，可以当作兼职赚取零花钱！过了推广阶段后，想要成为特约用户会比较难了，到时候会有新的规定出来。"
            });
            Questions.Add(new HelpModel
            {
                ID = "4",
                Question = "发现里移动网赚下面的应用不会是骗人的吧？",
                Explain = "这里面的都是一些应用通过回馈客户的方式赚点零花钱，数额很小，且不需要您缴纳任何费用，没办法骗到您的，请放心使用！"
            });
            Questions.Add(new HelpModel
            {
                ID = "5",
                Question = "什么是淘宝客？",
                Explain = "淘宝客是阿里妈妈旗下的一个推广功能。商家可以通过发放优惠券，并给出佣金，由淘宝客们帮忙推广，根据购买情况，发放给淘宝客佣金。是最容易入门且效果最好的一个兼职工作。如果您想成为淘宝客，赶紧联系我们吧！"
            });
            Questions.Add(new HelpModel
            {
                ID = "6",
                Question = "如何成为淘宝客？",
                Explain = "联系我们，我们会提供相应的方式方法！联系方式：微信：ydxc608,邮箱：publicresource@126.com"
            });
            Questions.Add(new HelpModel
            {
                ID = "7",
                Question = "上传商品有什么要求么？如何上传？",
                Explain = "不得包含任何非商品的广告用词！不得包含二维码！不得上传与商品无关的图片！不得上传法律或道德禁止的图片！违者，视情况严重性做相应处分！推荐使用淘宝客商品中的主图！"
            });
            Questions.Add(new HelpModel
            {
                ID = "8",
                Question = "怎样可以提高自己分享商品的点击率和成交率？",
                Explain = "多使用[闪荐]APP上传商品，推荐商品是按照上传时间展示的，不会出现任何置顶商品！写清楚明白分享理由，让用户了解购买的价值！多用引导用语，引导用户可以先领券，买或者不买后面再定！"
            });
            Questions.Add(new HelpModel
            {
                ID = "9",
                Question = "为什么我分享的商品过段时间就会不见了？",
                Explain = "这是因为淘宝客上的优惠券都会有时间限制的，过期之后，会有人工进行定期删除！只会删除过期的优惠券，所以请您放心！"
            });
        }
    }
}