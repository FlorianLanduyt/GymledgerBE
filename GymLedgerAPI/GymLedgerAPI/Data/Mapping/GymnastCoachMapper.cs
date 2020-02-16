using System;
using GymLedgerAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLedgerAPI.Data.Mapping
{
    public class GymnastCoachMapper : IEntityTypeConfiguration<GymnastCoach>
    {
        public void Configure(EntityTypeBuilder<GymnastCoach> builder)
        {
            builder.ToTable("GymnastCoach");

            builder.HasKey(gc => new { gc.GymnastId, gc.CoachId });

            builder.HasOne(gc => gc.Gymnast).WithMany().HasForeignKey(g => g.GymnastId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(gc => gc.Coach).WithMany(g => g.GymnastCoaches).HasForeignKey(c => c.CoachId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
