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

        [HttpGet]
        public async Task<object> GetCoupons(string q, long? pageNo, long? pageSize)
        {
            string url = "http://gw.api.taobao.com/router/rest";
            string appkey = "24620377";
            string appsecret = "f1169988a1703d26729664cf86b4b2ea";
            string format = "json";
            ITopClient client = new DefaultTopClient(url, appkey, appsecret,format);
            TbkDgItemCouponGetRequest req = new TbkDgItemCouponGetRequest();
            req.AdzoneId = 132554802L;
            req.Q = q;
            req.PageNo = pageNo ?? 1;
            req.PageSize = pageSize ?? 24;
            TbkDgItemCouponGetResponse response = client.Execute(req);

            return response.Body;
        }


    }
}