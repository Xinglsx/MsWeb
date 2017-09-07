using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MsWeb.DataObjects;
using MsWeb.Domains;

using Winning.Framework.DMSP.Services.Mapper;

namespace MsWeb.Services.Mapper
{
    public class DtoMapper : BaseMapperConfiguration
    {
        public override void InitBind()
        {
            this.BindByBaseType();
        }
    }
}
