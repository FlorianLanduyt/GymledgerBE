using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class TrainingExercise
    {
        public long TrainingId { get; set; }
        public Training Training { get; set; }
        public long ExerciseId { get; set; }
        public Exercise Exercise { get; set; }


        public TrainingExercise(long tId, Training t, long eId, Exercise e)
        {
            Training = t;
            TrainingId = tId;
            Exercise = e;
            ExerciseId = eId;
        }
    }
}
