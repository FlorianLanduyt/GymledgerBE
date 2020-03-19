using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Gymnast : User
    {

        //public int GymnastId { get; set; }
        public IList<Training> Trainings { get; set; } = new List<Training>();
        public IList<GymnastCoach> GymnastCoaches { get; set; } = new List<GymnastCoach>();

        public Gymnast(string firstname, string lastname, DateTime birthday, string email): base(firstname, lastname, birthday, email)
        {
            //Trainings = new List<Training>();
            //GymnastCoaches = new List<GymnastCoach>();
            isCoach = false;
        }

        protected Gymnast()
        {

        }

        public void AddCoach(Coach coach)
        {
            GymnastCoaches.Add(new GymnastCoach(this, coach));
        }

        public void RemoveGymnastFromCoach(Coach coach)
        {
            GymnastCoaches.Remove(GymnastCoaches.FirstOrDefault(gc => gc.Coach == coach));
        }

        public void AddTraining(Training training) {
            Trainings.Add(training);
        }

        public void RemoveTraining(Training training) {
            Trainings.Remove(training);
        }


    }
}
