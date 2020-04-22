using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class ExerciseEvaluation
    {
        public int Id { get; set; }
        public int DifficultyScore { get; set; }
        public string Note { get; set; }
        public int Series { get; set; }
        public double Weight { get; set; }
        public int Repetitions { get; set; }


        public Training Training { get; set; }
        public Exercise Exercise { get; set; }

        public ExerciseEvaluation(string note, int difficultyScore, double weight, int repetitions, int series, Exercise e)
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
