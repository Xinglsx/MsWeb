using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MsWeb.Controllers
{
    public class ProductServiceController : Controller
    {
        // GET: Productervice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AndroidApp()
        {
            ViewBag.Title = "MSKJ-Android手机APP开发";
            return View("~/Views/ProductService/AndroidApp.cshtml");
        }

        public ActionResult Hardware()
        {
            ViewBag.Title = "MSKJ-计算机相关硬件销售、电脑组装";
            return View("~/Views/ProductService/Hardware.cshtml");
        }

        public ActionResult Software()
        {
            ViewBag.Title = "MSKJ-软件开发";
            return View("~/Views/ProductService/Software.cshtml");
        }

        public ActionResult Training()
        {
            ViewBag.Title = "MSKJ-软件培训";
            return View("~/Views/ProductService/Training.cshtml");
        }

        public ActionResult WebSite()
        {
            ViewBag.Title = "MSKJ-公司主页、产品宣传页开发";
            return View("~/Views/ProductService/WebSite.cshtml");
        }

        public ActionResult WebSiteSEO()
        {
            ViewBag.Title = "MSKJ-网站优化SEO";
            return View("~/Views/ProductService/WebSiteSEO.cshtml");
        }
    }
}