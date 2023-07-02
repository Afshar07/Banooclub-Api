using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultantConsultCategoryConfiguration : IEntityTypeConfiguration<ConsultantConsultCategory>
    {
        public void Configure(EntityTypeBuilder<ConsultantConsultCategory> builder)
        {
            builder.ToTable("ConsultantConsultCategories", "Consulting");
            builder.HasKey(x => new
            {
                x.ConsultantId,
                x.ConsultCategoryId
            });



        }
    }
}
