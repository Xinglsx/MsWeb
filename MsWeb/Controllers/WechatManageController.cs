using MsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MsWeb.Controllers
{
    public class WechatManageController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "首页-微信管理平台";
            return View();
        }

        public ActionResult LoginAuthorise()
        {
            ViewBag.Title = "授权登录-微信管理平台";
            return View("~/Views/WechatManage/LoginAuthorise.cshtml");
        }
    }
}