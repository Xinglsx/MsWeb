using System;
using Mingshu.Framework.MSWeb.Core.BaseDataObject;

namespace MsWeb.DataObjects
{

    public partial class UserloginrecordModel : Model<string>
    {

        public string userid { get; set; }

        public DateTime? logintime { get; set; }

        public string loginaddress { get; set; }
    }
}
