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
            return View();
        }

        public ActionResult FindCoupon()
        {
            return View("~/Views/Coupon/FindCoupon.cshtml");
        }

        public ActionResult UploadCoupon()
        {
            return View("~/Views/Coupon/UploadCoupon.cshtml");
        }
    }
}