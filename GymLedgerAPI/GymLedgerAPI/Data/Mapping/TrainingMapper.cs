using System;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLedgerAPI.Data.Mapping
{
    public class TrainingMapper : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.ToTable("Training");

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.ExerciseEvaluations).WithOne().HasForeignKey(t => t.TrainingId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.TrainingExercises).WithOne().HasForeignKey( t=>t.TrainingId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
