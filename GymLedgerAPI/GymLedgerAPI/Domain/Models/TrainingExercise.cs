using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class TrainingExercise
    {
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }


        public TrainingExercise(Training t, Exercise e)
        {
            Training = t;
            TrainingId = t.Id;
            Exercise = e;
            ExerciseId = e.Id;
        }

        protected TrainingExercise()
        {

        }
    }
}
