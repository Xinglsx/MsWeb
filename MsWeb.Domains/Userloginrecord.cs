using System;
using Winning.Framework.DMSP.Core.BaseDomain;

namespace MsWeb.Domains
{

    public partial class Userloginrecord : EntityKeyedString
    {

        public string userid { get; set; }

        public DateTime? logintime { get; set; }

        public string loginaddress { get; set; }
    }
}
