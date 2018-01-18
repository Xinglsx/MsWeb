using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MsWeb.Controllers
{
    public class IntegralPlatformController : Controller
    {
        // GET: IntegralPlatform
        public ActionResult Index()
        {
            ViewBag.Title = "首页-积分平台";
            return View();
        }
    }
}