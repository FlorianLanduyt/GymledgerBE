using System;
using System.Diagnostics.CodeAnalysis;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class ExerciseEvaluation
    {
        private string _note;
        private int _difficultyScore;
        private double _weight;
        private int _repetitions;
        private int _series;

        public int Id { get; set; }

        [AllowNull]
        public int? DifficultyScore {
            get => _difficultyScore;
            set => _difficultyScore = value ?? 0;
        }

        [AllowNull]
        public string Note {
            get => _note;
            set => _note = value ?? "";
        }

        [AllowNull]
        public int? Series {
            get => _series;
            set => _series = value ?? 0;
        }

        [AllowNull]
        public double? Weight {
            get => _weight;
            set => _weight = value ?? 0;
        }

        [AllowNull]
        public int? Repetitions {
            get => _repetitions;
            set => _repetitions = value ?? 0;
        }


        public Training Training { get; set; }
        public Exercise Exercise { get; set; }

        public ExerciseEvaluation(string note, int? difficultyScore, double? weight, int? repetitions, int? series, Exercise e)
        {
            DifficultyScore = difficultyScore;
            Note = note;
            Weight = weight;
            Repetitions = repetitions;
            Series = series;

            Exercise = e;
        }

        protected ExerciseEvaluation()
        {

        }
    }
}
