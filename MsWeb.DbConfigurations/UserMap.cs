using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MsWeb.Domains;

using Winning.Framework.DMSP.DataAccess.Configurations;

namespace MsWeb.DbConfigurations
{
    public class UserMap : DbEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("Users");

            this.Property(x => x.Name).HasMaxLength(32);

            this.Property(x => x.LoginName).HasMaxLength(32);

            this.Property(x => x.Password).HasMaxLength(32);

            this.Property(x => x.Email).HasMaxLength(32);

            this.Property(x => x.Phone).HasMaxLength(32);

            this.Property(x => x.RowTimestamp).IsRowVersion();

            this.HasIndex(x => x.LoginName, new IndexAttribute() { IsUnique = true });
        }
    }
}
