using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Training {
        private int _amount;

        public int Id { get; set; }
        public KindOfTraining Kind { get; set; }
        public DateTime Date { get; set; }
        public string FeelingBeforeTraining { get; set; }
        public string FeelingAfterTraining { get; set; }

        public Gymnast Gymnast { get; set; }

        public IList<TrainingExercise> TrainingExercises { get; set; } = new List<TrainingExercise>();
        public IList<ExerciseEvaluation> ExerciseEvaluations { get; set; } = new List<ExerciseEvaluation>();

        //public int AmountOfExercises {
        //    get { return _amount; }
        //    private set {
        //        if (TrainingExercises.Any())
        //            _amount = TrainingExercises.Count;
        //        else
        //            _amount = 0;
        //    }
        //}

        public Training(KindOfTraining kind, DateTime date, string feelingBefore = "", string feelingAfter = "")
        {
            Kind = kind;
            Date = date;
            FeelingBeforeTraining = feelingBefore;
            FeelingAfterTraining = feelingAfter;
            //TrainingExercises = new List<TrainingExercise>();
            //ExerciseEvaluations = new List<ExerciseEvaluation>();
        }

        protected Training() {
        }

        public void AddExerciseToTraining(Exercise e) {
            TrainingExercises.Add(new TrainingExercise(this, e));
        }
    }
}
