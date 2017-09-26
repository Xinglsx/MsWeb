using MsWeb.Domains;
using Winning.Framework.DMSP.DataAccess.Configurations;

namespace MsWeb.DbConfigurations
{
    public class AdvertisementsMap : DbEntityTypeConfiguration<Advertisements>
    {
        public AdvertisementsMap()
        {
            this.ToTable("Advertisements");

            this.Property(e => e.ID)
                .HasColumnName("id")
                .HasMaxLength(36);

            this.Property(e => e.key)
                .HasMaxLength(36);

            this.Property(e => e.img)
                .HasMaxLength(256);

            this.Property(e => e.content)
                .HasColumnType("text");

            this.Property(e => e.goodskey)
                .HasMaxLength(36);
        }
    }
}
