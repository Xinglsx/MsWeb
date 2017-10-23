using System.Threading.Tasks;
using System.Web.Mvc;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace MsWeb.Controllers
{
    public class CouponController : Controller
    {
        // GET: Tichet
        public ActionResult Index()
        {
            ViewBag.Title = "闪荐-寻找更便宜的好东西";
            return View();
        }

        public ActionResult FindCoupon()
        {
            ViewBag.Title = "闪荐-淘宝天猫优惠券搜索";
            return View("~/Views/Coupon/FindCoupon.cshtml");
        }

        public ActionResult UploadCoupon()
        {
            ViewBag.Title = "闪荐-上传分享商品";
            return View("~/Views/Coupon/UploadCoupon.cshtml");
        }
    }
}