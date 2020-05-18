using System;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLedgerAPI.Data.Mapping
{
    public class ExerciseMapper : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercise");

            builder.HasKey(e => e.Id);

            //builder.HasOne(e => e.Gymnast).WithMany().OnDelete(DeleteBehavior.SetNull);

        }
    }
}
