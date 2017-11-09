using MsWeb.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MsWeb.Controllers
{
    public class SharePageController : Controller
    {
        // GET: SharePage
        public ActionResult Index()
        {
            ViewBag.Title = "闪荐-你有三个红包待领取";

            return View();
        }

        public ActionResult SingleGoods(string id)
        {
            ViewBag.Title = "闪荐-你有三个红包待领取";
            object model = new GoodsModel { ID = id};
            return View("~/Views/SharePage/SingleGoods.cshtml",model);
        }
    }
}