using Mingshu.Framework.MSWeb.Core.BaseDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsWeb.Domains
{
    public partial class WechatToken : EntityKeyedString
    {
        public string token { get; set; }
        public DateTime updatetime { get; set; }
    }
}
