using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Exercise
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public string Image { get; set; }
        public double Weight { get; set; }

        public IList<TrainingExercise> TrainingExercises{ get; set; }


        public Exercise(string description, int number, string image, double weight, string note = "")
        {
            Description = description;
            Number = number;
            Note = note;
            Image = image;
            Weight = weight;

            TrainingExercises = new List<TrainingExercise>();
        }

        protected Exercise()
        {

        }
    }
}
