using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class BecomeConsultantRequestScheduleConfiguration : IEntityTypeConfiguration<BecomeConsultantRequestSchedule>
    {
        public void Configure(EntityTypeBuilder<BecomeConsultantRequestSchedule> builder)
        {
            builder.ToTable("BecomeConsultantRequestSchedules", "Consulting");
            builder.HasOne(t => t.BecomeConsultantRequest).WithMany(t => t.BecomeConsultantRequestSchedules).HasForeignKey(t => t.BecomeConsultantRequestId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
