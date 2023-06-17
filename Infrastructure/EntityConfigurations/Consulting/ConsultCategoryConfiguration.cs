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

    public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant>
    {
        public void Configure(EntityTypeBuilder<Consultant> builder)
        {
            builder.ToTable("Consultants", "Consulting");
            builder.HasKey(x => x.Id);
        }
    }

    public class BecomeConsultantRequestConfiguration : IEntityTypeConfiguration<BecomeConsultantRequest>
    {
        public void Configure(EntityTypeBuilder<BecomeConsultantRequest> builder)
        {
            builder.ToTable("BecomeConsultantRequests", "Consulting");
            builder.HasKey(x => x.Id);
        }
    }

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
