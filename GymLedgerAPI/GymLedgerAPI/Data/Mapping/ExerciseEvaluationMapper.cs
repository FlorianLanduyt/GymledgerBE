using System;
using GymLedgerAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLedgerAPI.Data.Mapping
{
    public class ExerciseEvaluationMapper : IEntityTypeConfiguration<ExerciseEvaluation>
    {
        public void Configure(EntityTypeBuilder<ExerciseEvaluation> builder)
        {
            builder.ToTable("ExerciseEvaluation");

            //builder.HasKey(e => new { e.Exercise.Id, e.TrainingId });
        }
    }
}
