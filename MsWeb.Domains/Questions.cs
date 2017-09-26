using System;
using Winning.Framework.DMSP.Core.BaseDomain;

namespace MsWeb.Domains
{
    public partial class Questions : EntityKeyedString
    {

        public string feedbackuserid { get; set; }

        public string feedbackusernickname { get; set; }

        public DateTime? feedbacktime { get; set; }

        public string contact { get; set; }

        public string content { get; set; }
    }
}
