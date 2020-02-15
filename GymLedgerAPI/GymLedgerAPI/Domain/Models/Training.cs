using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Training
    {
        public long Id { get; set; }
        public KindOfTraining Kind { get; set; }
        public DateTime Date { get; set; }


        public IList<TrainingExercise> TrainingExercises { get; set; }

        public Training(KindOfTraining kind, DateTime date)
        {
            Kind = kind;
            Date = date;
            Exercises = new List<TrainingExercise>();
        }

        protected Training() { }
    }
}
