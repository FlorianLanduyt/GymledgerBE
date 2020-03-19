using System;
using GymLedgerAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLedgerAPI.Data.Mapping
{
    public class TrainingExerciseMapper : IEntityTypeConfiguration<TrainingExercise>
    {

        public void Configure(EntityTypeBuilder<TrainingExercise> builder)
        {
            builder.ToTable("TrainingExercise");

            builder.HasKey(te => new { te.TrainingId, te.ExerciseId });

            builder.HasOne(te => te.Training).WithMany(te => te.TrainingExercises).HasForeignKey(te => te.TrainingId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(te => te.Exercise).WithMany(te => te.TrainingExercises).HasForeignKey(te => te.ExerciseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
