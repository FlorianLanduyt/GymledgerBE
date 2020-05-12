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

            builder.HasOne(gc => gc.Gymnast).WithMany(g => g.GymnastCoaches).HasForeignKey(g => g.GymnastId).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(gc => gc.Coach).WithMany(g => g.GymnastCoaches).HasForeignKey(c => c.CoachId).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
