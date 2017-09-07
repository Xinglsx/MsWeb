using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Winning.Framework.DMSP.Core.BaseDomain;

namespace MsWeb.Domains
{
    public class User : AggregateRoot
    {
        public string Name { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
