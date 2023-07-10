using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class BecomeConsultantRequestConsultPriceConfiguration : IEntityTypeConfiguration<BecomeConsultantRequestConsultPrice>
    {
        public void Configure(EntityTypeBuilder<BecomeConsultantRequestConsultPrice> builder)
        {
            builder.ToTable("BecomeConsultantRequestConsultPrices", "Consulting");
            builder.HasKey(t => new { t.BecomeConsultantRequestId, t.Type });
            builder.HasOne(t => t.BecomeConsultantRequest).WithMany(t => t.BecomeConsultantRequestConsultPrices).HasForeignKey(t => t.BecomeConsultantRequestId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
