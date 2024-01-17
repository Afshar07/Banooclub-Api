using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultantVideoConferanceUrlConfiguration: IEntityTypeConfiguration<ConsultantVideoConferanceUrl>
    {
        public void Configure(EntityTypeBuilder<ConsultantVideoConferanceUrl> builder)
        {
            builder.ToTable("ConsultantVideoConferanceUrls", "Consulting");
            builder.HasKey(t => new { t.OrderId, t.ConsultantId });
            builder.HasOne(t => t.Consultant).WithMany(t => t.ConsultantVideoConferanceUrls).HasForeignKey(t => t.ConsultantId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.Order).WithMany(t => t.ConsultantVideoConferanceUrls).HasForeignKey(t => t.OrderId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(t => t.ConferanceUrl).IsRequired().HasMaxLength(300);
        }
    }
}
