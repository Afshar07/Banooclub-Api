using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultCategoryConfiguration : IEntityTypeConfiguration<ConsultCategory>
    {
        public void Configure(EntityTypeBuilder<ConsultCategory> builder)
        {
            builder.ToTable("ConsultCategories", "Consulting");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Title).IsRequired().HasMaxLength(250);
        }
    }
}
