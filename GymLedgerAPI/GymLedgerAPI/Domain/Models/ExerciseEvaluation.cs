using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class ExerciseEvaluation
    {
        public long Id { get; set; }
        public int DifficultyScore { get; set; }
        public string Note { get; set; }
        public Training Training { get; set; }
        public Exercise Exercise { get; set; }
        public long ExerciseId { get; set; }
        public long TrainingId { get; set; }

        public ExerciseEvaluation(String note, int score, Training t, Exercise e)
        {
            DifficultyScore = score;
            Note = note;
            Training = t;
            Exercise = e;
            ExerciseId = e.Id;
            TrainingId = t.Id;
        }
    }
}
