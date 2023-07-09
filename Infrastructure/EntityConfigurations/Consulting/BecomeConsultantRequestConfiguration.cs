using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class BecomeConsultantRequestConfiguration : IEntityTypeConfiguration<BecomeConsultantRequest>
    {
        public void Configure(EntityTypeBuilder<BecomeConsultantRequest> builder)
        {
            builder.ToTable("BecomeConsultantRequests", "Consulting");
            builder.HasKey(x => x.Id);
            builder.Property(t => t.Description).HasMaxLength(4000).IsRequired();
            builder.Property(t => t.NationalCode).HasMaxLength(15).IsRequired();
            builder.Property(t => t.MedicalSystemNumber).HasMaxLength(100).IsRequired();
            builder.Property(t => t.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(t => t.ShabaNo).HasMaxLength(40).IsRequired();
            builder.Property(t => t.BankName).HasMaxLength(100).IsRequired();
            builder.Property(t => t.CartNo).HasMaxLength(20).IsRequired();
        }
    }
}
