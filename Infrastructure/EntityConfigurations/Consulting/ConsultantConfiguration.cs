using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant>
    {
        public void Configure(EntityTypeBuilder<Consultant> builder)
        {
            builder.ToTable("Consultants", "Consulting");
            builder.HasKey(x => x.Id);

            builder.HasMany(t => t.ConsultantPrices).WithOne(t => t.Consultant).HasForeignKey(t => t.ConsultantId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(t => t.Categories).WithOne(t => t.Consultant).HasForeignKey(t => t.ConsultantId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.User).WithMany(t => t.Consultants).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.NoAction);

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
