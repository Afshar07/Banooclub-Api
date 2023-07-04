using BanooClub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BanooClub.Infrastructure.EntityConfigurations.Consulting
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities", "Social");

            builder.HasOne(t => t.User).WithMany(t => t.Activities).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
