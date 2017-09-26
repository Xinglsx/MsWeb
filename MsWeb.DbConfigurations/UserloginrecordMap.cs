using MsWeb.Domains;
using Winning.Framework.DMSP.DataAccess.Configurations;

namespace MsWeb.DbConfigurations
{
    public class UserloginrecordMap : DbEntityTypeConfiguration<Userloginrecord>
    {
        public UserloginrecordMap()
        {
            this.ToTable("Userloginrecord");

            this.Property(e => e.ID)
                .HasColumnName("id")
                .HasMaxLength(36);

            this.Property(e => e.userid)
                .HasMaxLength(36);

            this.Property(e => e.loginaddress)
                .HasMaxLength(36);

        }
    }
}
