using Mingshu.Framework.MSWeb.Core.BaseDataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsWeb.DataObjects.Model
{
    /// <summary>
    /// 微信Token信息
    /// </summary>
    public class WechatTokenModel : Model<string>
    {
        public string token { get; set; }
        public DateTime updatetime { get; set; }
    }
}
