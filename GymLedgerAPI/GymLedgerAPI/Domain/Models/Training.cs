using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Training
    {
        public int Id { get; set; }
        public KindOfTraining Kind { get; set; }
        public DateTime Date { get; set; }

        public IList<TrainingExercise> TrainingExercises { get; set; }
        public IList<ExerciseEvaluation> ExerciseEvaluations { get; set; }

        public Training(KindOfTraining kind, DateTime date)
        {
            Kind = kind;
            Date = date;
            TrainingExercises = new List<TrainingExercise>();
            ExerciseEvaluations = new List<ExerciseEvaluation>();
        }

        protected Training() { }
    }
}
