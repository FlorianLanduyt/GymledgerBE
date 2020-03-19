using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Training {
        private int _amount;

        public int Id { get; set; }
        public Category Category { get; set; }
        //public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public int FeelingBeforeTraining { get; set; }
        public int FeelingAfterTraining { get; set; }


        public Gymnast Gymnast { get; set; }

        public IList<TrainingExercise> TrainingExercises { get; set; } = new List<TrainingExercise>();
        public IList<ExerciseEvaluation> ExerciseEvaluations { get; set; } = new List<ExerciseEvaluation>();

        public int AmountOfExercises {
            get { return _amount; }
            private set { _amount = TrainingExercises.Count; }
        }



        public Training(Category category, DateTime date, int feelingBefore = 0, int feelingAfter = 0)
        {
            Category = category;
            //CategoryId = Category.Id;
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
            Console.WriteLine(TrainingExercises.Count);
        }
    }
}
