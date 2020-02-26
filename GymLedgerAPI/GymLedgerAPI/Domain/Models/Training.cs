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
        public string FeelingBeforeTraining { get; set; }
        public string FeelingAfterTraining { get; set; }

        public IList<TrainingExercise> TrainingExercises { get; set; }
        public IList<ExerciseEvaluation> ExerciseEvaluations { get; set; }

        public Training(KindOfTraining kind, DateTime date, string feelingBefore = "", string feelingAfter = "")
        {
            Kind = kind;
            Date = date;
            FeelingBeforeTraining = feelingBefore;
            FeelingAfterTraining = feelingAfter;
            TrainingExercises = new List<TrainingExercise>();
            ExerciseEvaluations = new List<ExerciseEvaluation>();
        }

        protected Training() { }

        public void AddExerciseToTraining(Exercise e) {
            TrainingExercises.Add(new TrainingExercise(this, e));
        }
    }
}
