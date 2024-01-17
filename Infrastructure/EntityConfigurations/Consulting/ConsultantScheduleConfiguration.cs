using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultantScheduleConfiguration : IEntityTypeConfiguration<ConsultantSchedule>
    {
        public void Configure(EntityTypeBuilder<ConsultantSchedule> builder)
        {
            builder.ToTable("ConsultantSchedule", "Consulting");
            builder.HasOne(t => t.Consultant).WithMany(t => t.ConsultantSchedules).HasForeignKey(t => t.ConsultantId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
