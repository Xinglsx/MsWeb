using MsWeb.DataObjects;
namespace MsWeb.Core.Utils
{
    public class TaobaoUtil
    {
        /// <summary>
        /// 解析淘宝客转发文字
        /// </summary>
        /// <param name="tbkStr">文字</param>
        /// <returns>返回解析后的商品信息</returns>
        public static GoodsModel AnalysisTbkStr(string tbkStr)
        {
            /*
             * 2018-06-14 文字格式
             * 俞兆林纯棉短袖t恤男全棉圆领韩版潮流修身男士大码宽松半袖体恤【包邮】
             * 【在售价】19.90元
             * 【券后价】14.90元
             * 【下单链接】http://m.tb.cn/h.307A6dG
             * -----------------
             * 復·制这段描述，€xeTl0xjGBdQ€ ，咑閞【手机淘宝】即可查看
             * 
             * 车载手机架多功能通用款
             * 【在售价】16.9元
             * 【拼团价】14.4元 2人购买即可享优惠哦~
             * 【下单链接】http://m.tb.cn/h.30kQSzk
             * -----------------
             * 復·制这段描述，€e8JD0xn6AOd€ ，咑閞【手机淘宝】即可查看
             * 
             */
            GoodsModel goods = new GoodsModel();
            if(tbkStr.Length > 0)
            {
                try
                {

                    int startIndex = 0;
                    int endIndex = tbkStr.IndexOf("【在售价】") - 1;
                    if (endIndex > 0 && endIndex > startIndex)
                    {
                        goods.description = tbkStr.Substring(startIndex, endIndex- startIndex);
                    }

                    if (tbkStr.Contains("【券后价】"))
                    {
                        startIndex = tbkStr.IndexOf("【在售价】") + 5;
                        endIndex = tbkStr.IndexOf("【券后价】") - 2;
                        if (startIndex >= 0 && endIndex > startIndex)
                        {
                            LogUtil.WebLog(string.Format("oldprice:{0}", goods.oldprice));
                        }

                        startIndex = tbkStr.IndexOf("【券后价】") + 5;
                        endIndex = tbkStr.IndexOf("【下单链接】") - 2;
                        if (startIndex >= 0 && endIndex > startIndex)
                        {
                            LogUtil.WebLog(string.Format("price:{0}", goods.price));
                        }
                    }
                    else if (tbkStr.Contains("【拼团价】"))
                    {
                        startIndex = tbkStr.IndexOf("【在售价】") + 5;
                        endIndex = tbkStr.IndexOf("【拼团价】") - 2;
                        if (startIndex >= 0 && endIndex > startIndex)
                        {
                            goods.oldprice = "￥" + tbkStr.Substring(startIndex, endIndex - startIndex);
                        }

                        startIndex = tbkStr.IndexOf("【拼团价】") + 5;
                        endIndex = tbkStr.IndexOf("人购买即可享优惠") - 2;
                        if (startIndex >= 0 && endIndex > startIndex)
                        {
                            goods.price = "￥" + tbkStr.Substring(startIndex, endIndex - startIndex);
                        }
                    }

                    startIndex = tbkStr.IndexOf("【下单链接】") + 6;
                    endIndex = tbkStr.IndexOf("-----") - 1;
                    if (startIndex >= 0 && endIndex > 0 && endIndex > startIndex)
                    {
                        goods.link = tbkStr.Substring(startIndex, endIndex - startIndex);
                    }

                    startIndex = tbkStr.IndexOf("復·制这段描述") + 8;
                    endIndex = tbkStr.IndexOf("咑閞【手机淘宝】") - 2;
                    if (startIndex >= 0 && endIndex > 0 && endIndex > startIndex)
                    {
                        goods.command = tbkStr.Substring(startIndex, endIndex - startIndex);
                    }

                }
                catch (System.Exception exp)
                {
                    LogUtil.WebError(exp);
                }
            }
            return goods;
        }
    }
}
