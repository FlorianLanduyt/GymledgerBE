﻿using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public string Image { get; set; }
        public double Weight { get; set; }

        //public IList<Category> categories { get; set; } = new List<Category>();
        public IList<TrainingExercise> TrainingExercises{ get; set; } = new List<TrainingExercise>();


        public Exercise(string description, int number, string image, double weight)
        {
            Description = description;
            Number = number;
            Image = image;
            Weight = weight;
 
        }

        protected Exercise()
        {

        }
    }
}
