using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public IList<TrainingExercise> TrainingExercises{ get; set; } = new List<TrainingExercise>();
        public Gymnast Gymnast { get; set; }



        public Exercise(string description, string image, Gymnast gymnast)
        {
            Description = description;
            Image = image;
            Gymnast = gymnast;
        }

        protected Exercise()
        {

        }
    }
}
