using System;
using Winning.Framework.DMSP.Core.BaseDomain;

namespace MsWeb.Domains
{
    public partial class Users : EntityKeyedString
    {

        public string userid { get; set; }

        public string password { get; set; }

        public string nickname { get; set; }

        public string idcard { get; set; }

        public string realname { get; set; }

        public DateTime? birth { get; set; }

        public string phonenumber { get; set; }

        public string avatar { get; set; }

        public short? usertype { get; set; }

        public short? userlevel { get; set; }

        public string usersignature { get; set; }

        public string wechat { get; set; }

        public string qq { get; set; }

        public string sina { get; set; }

        public string taobao { get; set; }

        public DateTime? registertime { get; set; }
    }
}
