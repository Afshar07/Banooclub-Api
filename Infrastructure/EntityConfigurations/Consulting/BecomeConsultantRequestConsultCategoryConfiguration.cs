using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class BecomeConsultantRequestConsultCategoryConfiguration : IEntityTypeConfiguration<BecomeConsultantRequestConsultCategory>
    {
        public void Configure(EntityTypeBuilder<BecomeConsultantRequestConsultCategory> builder)
        {
            builder.ToTable("BecomeConsultantRequestConsultCategories", "Consulting");
            builder.HasKey(x => new
            {
                x.RequestId,
                x.CategoryId
            });
        }
    }
}
