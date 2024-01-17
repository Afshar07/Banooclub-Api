using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultantUserScheduleRatingConfiguration : IEntityTypeConfiguration<ConsultantUserScheduleRating>
    {
        public void Configure(EntityTypeBuilder<ConsultantUserScheduleRating> builder)
        {
            builder.ToTable("ConsultantUserScheduleRatings", "Consulting");
            builder
                .HasOne(t => t.ConsultantUserSchedule)
                .WithMany(t => t.ConsultantUserScheduleRatings)
                .HasForeignKey(t => t.ConsultantUserScheduleId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(t => t.Description).HasMaxLength(4000);
        }
    }
}
