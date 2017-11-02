using Mingshu.Framework.MSWeb.DataAccess.Configurations;
using MsWeb.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsWeb.DbConfigurations
{
    public class WechatTokenMap : DbEntityTypeConfiguration<WechatToken>
    {
        public WechatTokenMap()
        {
            this.ToTable("WechatToken");

            this.Property(e => e.ID).HasMaxLength(36);

            this.Property(e => e.token).HasMaxLength(512);
        }
    }
}
