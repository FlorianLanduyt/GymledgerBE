using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Training {
        private int _amount;
        private string _dayOfWeek;

        public int Id { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Day{
            get {
                return _dayOfWeek;
            }
            private set {
                switch (Date.DayOfWeek) {
                    case DayOfWeek.Monday: _dayOfWeek = "maandag"; break;
                    case DayOfWeek.Tuesday: _dayOfWeek = "dinsdag"; break;
                    case DayOfWeek.Wednesday: _dayOfWeek = "woensdag"; break;
                    case DayOfWeek.Thursday: _dayOfWeek = "donderdag"; break;
                    case DayOfWeek.Friday: _dayOfWeek = "vrijdag"; break;
                    case DayOfWeek.Saturday: _dayOfWeek = "zaterdag"; break;
                    case DayOfWeek.Sunday: _dayOfWeek = "zondag"; break;
                }
            }
            
        }
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
            var inList = TrainingExercises.ToList().SingleOrDefault(te => te.Exercise == e);

            if(inList == null) {
                TrainingExercises.Add(new TrainingExercise(this, e));
            } else {
                throw new Exception("Reeds in de lijst");
            }
        }

        public void AddExerciseEvaluationToTraining(ExerciseEvaluation exEvaluation) {
            exEvaluation.Training = this;

            var inList = ExerciseEvaluations
                .ToList()
                .SingleOrDefault(ee => (ee.Training == exEvaluation.Training) && (ee.Exercise == exEvaluation.Exercise));

            if(inList == null) {
                ExerciseEvaluations.Add(exEvaluation);
            } else {
                throw new Exception("Reeds in de lijst");
            }

        }


        public void DeleteExerciseFromTraining(Exercise exercise) {
            TrainingExercises.Remove(TrainingExercises.FirstOrDefault(te => te.Exercise == exercise));
        }
    }
}
