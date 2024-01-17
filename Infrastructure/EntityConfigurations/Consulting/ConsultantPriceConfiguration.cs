using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultantPriceConfiguration : IEntityTypeConfiguration<ConsultantPrice>
    {
        public void Configure(EntityTypeBuilder<ConsultantPrice> builder)
        {
            builder.ToTable("ConsultantPrice", "Consulting");
            builder.HasKey(t => new { t.ConsultantId, t.Type });
        }
    }
}
