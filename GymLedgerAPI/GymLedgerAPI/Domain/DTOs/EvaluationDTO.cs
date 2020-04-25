using System;
using System.Diagnostics.CodeAnalysis;

namespace GymLedgerAPI.Domain.DTOs {
    public class EvaluationDTO {
        [AllowNull]
        public int Id { get; set; }

        [AllowNull]
        public int? Sets { get; set; }

        [AllowNull]
        public int? Repetitions { get; set; }

        [AllowNull]
        public double? Weight { get; set; }

        [AllowNull]
        public string Note { get; set; }

        [AllowNull]
        public int? FeelingOfExercise { get; set; }
    }

}
