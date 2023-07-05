using BanooClub.Models.Consulting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ConsultantUserScheduleConfiguration : IEntityTypeConfiguration<ConsultantUserSchedule>
    {
        public void Configure(EntityTypeBuilder<ConsultantUserSchedule> builder)
        {
            builder.ToTable("ConsultantUserSchedules", "Consulting");
            builder.HasOne(t => t.User).WithMany(t => t.ConsultantUserSchedules).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.Consultant).WithMany(t => t.ConsultantUserSchedules).HasForeignKey(t => t.ConsultantId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.Order).WithMany(t => t.ConsultantUserSchedules).HasForeignKey(t => t.OrderId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(t => t.Description).HasMaxLength(4000);

        }
    }
}
