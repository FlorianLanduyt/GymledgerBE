using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class ExerciseEvaluation
    {
        public int Id { get; set; }
        public int DifficultyScore { get; set; }
        public string Note { get; set; }
        public Training Training { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int TrainingId { get; set; }

        public ExerciseEvaluation(String note, int score, Training t, Exercise e)
        {
            DifficultyScore = score;
            Note = note;
            Training = t;
            Exercise = e;
            ExerciseId = e.Id;
            TrainingId = t.Id;
        }

        protected ExerciseEvaluation()
        {

        }
    }
}
